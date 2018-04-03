var gl;

class TextureReader {
    constructor(url) {
        var texture = gl.createTexture();
        gl.bindTexture(gl.TEXTURE_2D, texture);

        var pixel = new Uint8Array([190, 190, 190, 255]);  // opaque blue
        gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA,
                1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE,
                pixel);

        var image = new Image();
        image.onload = function () {
            gl.bindTexture(gl.TEXTURE_2D, texture);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA,
                    gl.RGBA, gl.UNSIGNED_BYTE, image);

            // WebGL1 has different requirements for power of 2 images
            // vs non power of 2 images so check if the image is a
            // power of 2 in both dimensions.
            if ((image.width & (image.width - 1)) == 0 && (image.height & (image.height - 1)) == 0) {
                // Yes, it's a power of 2. Generate mips.
                gl.generateMipmap(gl.TEXTURE_2D);
            } else {
                // No, it's not a power of 2. Turn of mips and set
                // wrapping to clamp to edge
                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
            }
        };
        
        image.src = url;
    }
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
            attribute vec3 aNormal;
            attribute vec2 aTextCoord;
        
            uniform mat4 uModelMatrix;
            uniform mat4 uViewMatrix;
            uniform mat4 uProjectionMatrix;
            
            varying vec3 vNormal;
            varying vec3 vPosition;
            varying vec2 vTextCoord;
            
            void main() {
                gl_Position = uProjectionMatrix * uViewMatrix * uModelMatrix * aVertexPosition;
                vPosition = vec3(uModelMatrix * aVertexPosition);
                vNormal = aNormal;
                vTextCoord = aTextCoord;
            }
        `;        
    }
}

class FragmentShader extends Shader {
    constructor() {
        super();
        this.type = gl.FRAGMENT_SHADER;
        this.source = `
            precision highp float;  
       
            uniform sampler2D uTexture;
        
            varying vec3 vNormal;
            varying vec3 vPosition;
            varying vec2 vTextCoord;
        
            void main(void){
                vec3 lightColor = vec3(1.0, 1.0, 1.0);
                float ambientStrength = 0.5;
                vec3 ambient = ambientStrength * lightColor;
                        
                vec3 lightPosition = vec3(0.0, 10.0, 6.0);        
                vec3 normal = normalize(vNormal);
                vec3 lightDirection = normalize(lightPosition - vPosition);
                float diff = max(dot(normal, lightDirection), 0.0);
                vec3 diffuse = diff * lightColor;
                
                vec4 texel = texture2D(uTexture, vTextCoord);
                
                vec3 result = (ambient + diffuse) * texel.rgb;
                
                gl_FragColor = vec4(result, texel.a);
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
        var translation = [0.0, 0.0, 0.0];
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
        };

        this.getRotation = function() {
            return rotation;
        };

        this.getScaling = function() {
            return scaling;
        };

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
    constructor(canvas) {
        console.log("Canvas element:", canvas);
        
        //inicialize with standart webGl or experimental if the first one is not enabled
        //gl = canvas.getContext("webgl") | canvas.getContext("experimental-webgl");
        gl = canvas.getContext("webgl");
        if(!gl){
          alert("Unable to initialize webgl context");
        }
    }

    startAnimation(camera, vertices, faces, fps) {
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

            var positionLocation = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            var normalLocation = gl.getAttribLocation(shaderProgram, "aNormal");
            var textureCoordLocation = gl.getAttribLocation(shaderProgram, 'aTextCoord');
            var projectionMatrixLocation = gl.getUniformLocation(shaderProgram, 'uProjectionMatrix');
            var viewMatrixLocation = gl.getUniformLocation(shaderProgram, 'uViewMatrix');
            var modelMatrixLocation = gl.getUniformLocation(shaderProgram, 'uModelMatrix');
            var textureLocation = gl.getUniformLocation(shaderProgram, 'uTexture');
        }

        // --------------------- Initialize buffers ---------------------
        {
            console.log("Initialize buffers");

            var positionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);

            var indicesBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicesBuffer);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(faces), gl.STATIC_DRAW);

            var normalBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
            
            var textureCoordBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices.textureCoords), gl.STATIC_DRAW);
            
            var projectionMatrix = mat4.create();
            mat4.perspective(projectionMatrix, Math.PI / 4, gl.canvas.clientWidth / gl.canvas.clientHeight, 0.1, 100.0);

            var viewMatrix = mat4.create();
            mat4.translate(viewMatrix, viewMatrix, [0, 0, -6]);
        }

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
                gl.enableVertexAttribArray(positionLocation);
                gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
                gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices.vertices[animationIndex]), gl.DYNAMIC_DRAW);
                gl.vertexAttribPointer(positionLocation, 3, gl.FLOAT, false, 0, 0);

                gl.enableVertexAttribArray(normalLocation);
                gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
                gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices.normals[animationIndex]), gl.DYNAMIC_DRAW);
                gl.vertexAttribPointer(normalLocation, 3, gl.FLOAT, false, 0, 0);
                
                // Tell WebGL which indices to use to index the vertices
                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicesBuffer);
                
                gl.enableVertexAttribArray(textureCoordLocation);
                gl.bindBuffer(gl.ARRAY_BUFFER, textureCoordBuffer);
                gl.vertexAttribPointer(textureCoordLocation, 2, gl.FLOAT, false, 0, 0);
            }
            
            gl.uniform1i(textureLocation, 0);
            
            // --------------------- Transformations ---------------------
            {
                var modelMatrix = mat4.create();
                mat4.translate(modelMatrix, modelMatrix, camera.getTranslation());
                mat4.rotateX(modelMatrix, modelMatrix, camera.getRotation()[0]);
                mat4.rotateY(modelMatrix, modelMatrix, camera.getRotation()[1]);
                mat4.rotateZ(modelMatrix, modelMatrix, camera.getRotation()[2]);
                mat4.scale(modelMatrix, modelMatrix, camera.getScaling());

                gl.uniformMatrix4fv(projectionMatrixLocation, false, projectionMatrix);
                gl.uniformMatrix4fv(viewMatrixLocation, false, viewMatrix);
                gl.uniformMatrix4fv(modelMatrixLocation, false, modelMatrix);
            }

            gl.drawElements(gl.TRIANGLES, faces.length, gl.UNSIGNED_SHORT, 0);

            var deltaTime = now - then;
            then = now;
            if(deltaTime < 1000 / fps) {
                 setTimeout(function(){
                    animationIndex = (animationIndex + 1) % vertices.vertices.length;
                    requestAnimationFrame(drawScene);
                 },1000 / fps - deltaTime);
            } else {
                animationIndex = (animationIndex + 1) % vertices.vertices.length;
                requestAnimationFrame(drawScene);
            }
        }
    }
}

//---------------------------------- Main function ----------------------------------
function main(canvas, path, texture){
    console.log("Starting webgl viewer");

    canvas = document.getElementById(canvas);
    var webgl = new Webgl(canvas);
    if (!gl) {
        return;
    }

    var camera = new CameraMovement(canvas);

    var myWorker = new Worker('preprocessing.js');
    myWorker.addEventListener('message', function (e) {
        webgl.startAnimation(camera, e.data[0], e.data[1], 25);
    }, false);
    myWorker.postMessage(path);

    new TextureReader(texture);  
}