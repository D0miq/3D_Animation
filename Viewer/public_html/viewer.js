var gl;

//---------------------------------- File reader ---------------------------------- DONE
class FileReader3BA {
    constructor(file){
        var position = 0;
        
        function getContent(numberOfBytes) {
            if (position < file.byteLength) {
                var content = file.slice(position, position + numberOfBytes);
                position = position + numberOfBytes;
                return content;
            } else {
                return null;
            }
        };
        
        this.readAverageTrajectory = function () {
            var trajectoryLength = new Int32Array(getContent(4))[0];
            console.log("Trajectory length", trajectoryLength);
            return Array.from(new Float32Array(getContent(trajectoryLength * 4)));
        };
        
        this.readEigenVectors = function () {
            var dataDimensions = new Int32Array(getContent(8));
            console.log("Eigen vectors rows length", dataDimensions[0]);
            console.log("Eigen vectors columns length", dataDimensions[1]);
            var eigenVectors = new Float32Array(getContent(dataDimensions[0] * dataDimensions[1] * 4));
            var matrixArray = [];
            for (var i = 0; i < dataDimensions[0]; i++) {
                matrixArray[i] = Array.from(eigenVectors.slice(dataDimensions[1] * i, dataDimensions[1] * (i + 1)));
            }

            return math.matrix(matrixArray, "dense", "number");
        };
        
        this.readControlTrajectories = function () {
            var dataDimensions = new Int32Array(getContent(8));
            console.log("Control trajectories rows length", dataDimensions[0]);
            console.log("Control trajectories columns length", dataDimensions[1]);
            var controlTrajectories = new Float32Array(getContent(dataDimensions[0] * dataDimensions[1] * 4));
            var matrixArray = [];
            for (var i = 0; i < dataDimensions[0]; i++) {
                matrixArray[i] = Array.from(controlTrajectories.slice(dataDimensions[1] * i, dataDimensions[1] * (i + 1)));
            }
            
            return math.matrix(matrixArray, "dense", "number");
        };
        
        this.readFaces = function () {
            var facesLength = new Int32Array(getContent(4))[0];
            console.log("Faces length", facesLength);
            var allFaces = new Int32Array(getContent(facesLength * 4 * 9));
            var j = 0;
            this.faces = [];
            for(var i = 0; i < allFaces.length; i += 3) {
                this.faces[j] = allFaces[i] - 1;
                j++;
            }
        };
        
        this.readTextures = function () {
            var texturesLength = new Int32Array(getContent(4))[0];
            console.log("Textures length", texturesLength);
            this.textures = texturesLength == 0 ? null : new Float32Array(getContent(texturesLength * 4));
        };
        
        this.readNormals = function () {
            var normalsLength = new Int32Array(getContent(4))[0];
            console.log("Normals length", normalsLength);
            this.normals = normalsLength == 0 ? null : new Float32Array(getContent(normalsLength * 4));
        };
    }
    
    readFile() {
        var averageTrajectory = this.readAverageTrajectory();
        var eigenVectors = this.readEigenVectors();
        console.log(eigenVectors);
        var controlTrajectories = this.readControlTrajectories();
        console.log(controlTrajectories);
        this.readFaces();
        this.readTextures();
        this.readNormals();
        
        var s = math.multiply(eigenVectors, controlTrajectories);
        var b = s.map(function (value, index, matrix) {
            return value + averageTrajectory[index[0]];
        });
        
        this.vertices = [];
        var size = math.size(b).valueOf();
        for(var i = 0; i < size[0]; i += 3) {
            var frame = [];
            for(var j = 0; j < size[1] * 3; j += 3) {
                frame[j] = b.subset(math.index(i,j / 3));
                frame[j + 1] = b.subset(math.index(i + 1,j / 3));
                frame[j + 2] = b.subset(math.index(i + 2,j / 3));
            }
            
            this.vertices[i / 3] = frame;
        }
   }
}

class TextureReader {
    
}

//---------------------------------- Shaders ----------------------------------
class Shader {
    get source() {
        return this._source;
    }
    
    set source(value) {
        this._source = value; 
    }
    
    get type() {
        return this._type;
    }
    
    set type(value) {
        this._type = value;
    }
    
    build() {
        console.log("Building shader");
        var shader = gl.createShader(this.type);
        //set shader source code
        gl.shaderSource(shader, this.source);
        gl.compileShader(shader);

        //check shader compile status
        if(!gl.getShaderParameter(shader, gl.COMPILE_STATUS)){
            console.log("An error occurred compiling the shaders:", gl.getShaderInfoLog(shader));
            gl.deleteShader(shader);
            return null;
        }
        
        return shader;
    }
}

class VertexShader extends Shader {
    constructor() {
        super();
        this.type = gl.VERTEX_SHADER;
        this.source = `
            attribute vec4 aVertexPosition;
            uniform mat4 uModelViewMatrix;
            uniform mat4 uProjectionMatrix;
        
            void main() {
                gl_Position = uProjectionMatrix * uModelViewMatrix * aVertexPosition;
            }
        `;
    }
}

class FragmentShader extends Shader {
    constructor() {
        super();
        this.type = gl.FRAGMENT_SHADER;
        this.source = `
            void main(void){
                gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0);
            }
        `;
    }
}

//---------------------------------- Camera ---------------------------------- DONE
class CameraMovement {
    constructor(canvas) { 
        var lastTranslationX = 0;
        var lastTranslationY = 0;
        var lastRotationX = 0;
        var lastRotationY = 0;
        var clickX = 0;
        var clickY = 0;
        var translation = [0.0, 0.0, -6.0];
        var rotation = [0, 0, 0];
        var scaling = [1, 1, 1];

        function movedTranslation(event) {
            if (event.buttons == 4) {
                translation = [lastTranslationX + (event.clientX - clickX) / canvas.width, lastTranslationY + (clickY - event.clientY) / canvas.height, translation[2]];
                console.log(translation);
            } else {
                lastTranslationX = translation[0];
                lastTranslationY = translation[1];
                canvas.removeEventListener("mousemove", movedTranslation);
            }
        }

        function movedRotation(event) {
            if (event.buttons == 1) {
                rotation = [lastRotationX + (clickY - event.clientY) * Math.PI / 180, lastRotationY + (event.clientX - clickX) * Math.PI / 180, rotation[2]];
                console.log(rotation);
            } else {
                lastRotationX = rotation[0];
                lastRotationY = rotation[1];
                canvas.removeEventListener("mousemove", movedRotation);
            }
        }
        
        this.getTranslation = function() {
            return translation;
        }
        
        this.getRotation = function() {
            return rotation;
        }
        
        this.getScaling = function() {
            return scaling;
        }
        
        canvas.onmousedown = function(event) {
            clickX = event.clientX;
            clickY = event.clientY;
            if (event.button == 0) {
                console.log("Left button");
                canvas.addEventListener("mousemove", movedRotation, false);
            } else if(event.button == 1) {
                console.log("Middle button");
                canvas.addEventListener("mousemove", movedTranslation, false);
            }
        };
        
        canvas.addEventListener("wheel", event => {
            if (event.deltaY < 0) {
                scaling[0] = scaling[0] - 0.1;
                scaling[1] = scaling[1] - 0.1;
                scaling[2] = scaling[2] - 0.1;
            } else {
                scaling[0] = scaling[0] + 0.1;
                scaling[1] = scaling[1] + 0.1;
                scaling[2] = scaling[2] + 0.1;
            }

            console.log(scaling);
        });
    }
}

//---------------------------------- Webgl ----------------------------------
class Webgl {
    constructor(canvas, camera, vertices, faces, textures, normals) {
        console.log("Canvas element:", canvas);
        this.camera = camera;
        this.vertices = vertices;
        this.faces = faces;
        this.textures = textures;
        if (normals == null) {
            this.normals = computeNormals(vertices, faces);
        } else {
            this.normals = normals;
        }

        //inicialize with standart webGl or experimental if the first one is not enabled
        //gl = canvas.getContext("webgl") | canvas.getContext("experimental-webgl");
        gl = canvas.getContext("webgl");
        if(!gl){
          alert("Unable to initialize webgl context");
        }
        
        function computeNormals(vertices, faces) {
            
        }
    }
    
    startAnimation() {
        // --------------------- Initialize shaders ---------------------
        {
            var fragmentShader = new FragmentShader();
            var vertexShader = new VertexShader();

            //create shader program
            var shaderProgram = gl.createProgram();
            gl.attachShader(shaderProgram, fragmentShader.build());
            gl.attachShader(shaderProgram, vertexShader.build());
            gl.linkProgram(shaderProgram);

            //if creating shader program failed
            if (!gl.getProgramParameter(shaderProgram, gl.LINK_STATUS)) {
                console.log("Unable to initialize the shader program:", gl.getProgramInfoLog(shaderProgram));
            }

            var vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            var projectionMatrixLocation = gl.getUniformLocation(shaderProgram, 'uProjectionMatrix');
            var modelViewMatrixLocation = gl.getUniformLocation(shaderProgram, 'uModelViewMatrix');
        }
        
        // --------------------- Initialize buffers ---------------------
        {
            console.log("Initialize buffers");

            var positionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
            
            var indicesBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicesBuffer);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(this.faces), gl.STATIC_DRAW);
        }
        
        var camera = this.camera;
        var faces = this.faces;
        var vertices = this.vertices;
        var animationIndex = 0;
        var then = 0;
        
        requestAnimationFrame(drawScene);
        
        function drawScene(now) {
            console.log("Drawing a scene");
            //set background
            gl.clearColor(0.0, 0.0, 0.0, 1.0);
            //enable depth testing
            gl.enable(gl.DEPTH_TEST);
            //near thing obscure far
            gl.depthFunc(gl.LEQUAL);
            //clear color and depth buffer
            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

            gl.useProgram(shaderProgram);
            
            // --------------------- Buffers ---------------------
            {
                gl.enableVertexAttribArray(vertexPositionAttribute);
                gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
                gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices[animationIndex]), gl.DYNAMIC_DRAW);
                gl.vertexAttribPointer(vertexPositionAttribute, 3, gl.FLOAT, false, 0, 0);
                
                // Tell WebGL which indices to use to index the vertices
                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicesBuffer);
            }
            
            // --------------------- Transformations ---------------------
            {
                var projectionMatrix = mat4.create();

                mat4.perspective(projectionMatrix, Math.PI / 4, gl.canvas.clientWidth / gl.canvas.clientHeight, 0.1, 100.0);

                var modelViewMatrix = mat4.create();
                mat4.translate(modelViewMatrix, modelViewMatrix, camera.getTranslation());
                mat4.rotateX(modelViewMatrix, modelViewMatrix, camera.getRotation()[0]);
                mat4.rotateY(modelViewMatrix, modelViewMatrix, camera.getRotation()[1]);
                mat4.rotateZ(modelViewMatrix, modelViewMatrix, camera.getRotation()[2]);
                mat4.scale(modelViewMatrix, modelViewMatrix, camera.getScaling());

                gl.uniformMatrix4fv(projectionMatrixLocation, false, projectionMatrix);
                gl.uniformMatrix4fv(modelViewMatrixLocation, false, modelViewMatrix);
            }
            
            gl.drawElements(gl.TRIANGLES, faces.length, gl.UNSIGNED_SHORT, 0);
            
            now *= 0.001;
            var deltaTime = now - then;
            then = now;
            animationIndex = Math.floor(animationIndex + 60 * deltaTime) % vertices.length;
            requestAnimationFrame(drawScene);
        }
    }
}

//---------------------------------- Main function ----------------------------------
function main(canvas, path, texture){
    console.log("Starting webgl viewer");
    
    console.log("Downloading file", path, "from the server");
    var request = new XMLHttpRequest();
    request.open("GET", path);
    request.responseType = "arraybuffer";
    request.onload = function (e) {
        var file = request.response;
        console.log(file);

        if (file) {
            var fileReader = new FileReader3BA(file);
            fileReader.readFile();

            canvas = document.getElementById(canvas);
            var camera = new CameraMovement(canvas);
            var webgl = new Webgl(canvas, camera, fileReader.vertices, fileReader.faces);
            
            if (!gl) {
                return;
            }

            webgl.startAnimation();
        } else {
            alert("Unable to read file " + this.path);
        }
    };

    request.send();
}