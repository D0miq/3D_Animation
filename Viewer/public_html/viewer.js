var gl;
var squareVerticesBuffer;   
var vertexPositionAttribute;

//---------------------------------- File reader ----------------------------------
class FileReader3abf {
    constructor(path){
        console.log("Open stream to file " + path);
        this.path = path;
        this.position = 0;
        
        function readFileFromServer(numberOfBytes) {
            console.log("File position " + this.position);
            console.log("Number of read bytes " + numberOfBytes);
            
            var fileContent;
            
            var request = new XMLHttpRequest();
            request.open("GET", this.path, true);
            request.responseType = "arraybuffer";
            request.setRequestHeader("Range", "bytes=" + this.position + "-" + numberOfBytes);
            request.onload = function (oEvent) {
                var arrayBuffer = this.request.response; // Note: not oReq.responseText
                if (arrayBuffer) {
                    fileContent = arrayBuffer;
                } else {
                    console.log("File " + this.path + " not read properly");
                }
            };
            
            this.position = this.position + numberOfBytes;
            
            return fileContent;
        }
        
        this.readAverageTrajectory = function () {
            var trajectoryLength = new Int32Array(readFileFromServer(4))[0];
            console.log("Trajectory length " + trajectoryLength);
            return new Float32Array(readFileFromServer(trajectoryLength * 4));
        }
        
        this.readEigenVectors = function () {
            var dataDimensions = new Int32Array(readFileFromServer(8));
            console.log("Eigen vectors rows length " + dataDimensions[0]);
            console.log("Eigen vectors columns length " + dataDimensions[1]);
            return new Float32Array(readFileFromServer(dataDimensions[0] * dataDimensions[1] * 4));
        };
        
        this.readControlTrajectories = function () {
            var dataDimensions = new Int32Array(readFileFromServer(8));
            console.log("Control trajectories rows length " + dataDimensions[0]);
            console.log("Control trajectories columns length " + dataDimensions[1]);
            return new Float32Array(readFileFromServer(dataDimensions[0] * dataDimensions[1] * 4));
        };
        
        this.readFaces = function () {
            var facesLength = new Int32Array(readFileFromServer(4))[0];
            console.log("Faces length " + facesLength);
            return new Float32Array(readFileFromServer(facesLength * 4));
        };
    }
    
    readFile() {
        this.averageTrajectory = this.readAverageTrajectory();
        this.eigenVectors = this.readEigenVectors();
        this.controlTrajectories = this.readControlTrajectories();
        this.faces = this.readFaces();
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
            console.log("An error occurred compiling the shaders: " + gl.getShaderInfoLog(shader));
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
            attribute vec3 aVertexPosition;

            void main(void){
              gl_Position = vec4(aVertexPosition, 1.0);
              gl_PointSize = 50.0;
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

//---------------------------------- Webgl ----------------------------------
class Webgl {
    constructor(canvas) {
        gl = null;
        
        console.log("Get canvas element by " + canvas + " id");
        canvas = document.getElementById(canvas);
        
        //inicialize with standart webGl or experimental if the first one is not enabled
        //gl = canvas.getContext("webgl") | canvas.getContext("experimental-webgl");
        gl = canvas.getContext("webgl");

        if(!gl){
          alert("Unable to initialize webgl context");
        }
    }
    
    initShaders() {
        var fragmentShader = new FragmentShader();
        var vertexShader = new VertexShader();

        //create shader program
        var shaderProgram = gl.createProgram();
        gl.attachShader(shaderProgram, fragmentShader.build());
        gl.attachShader(shaderProgram, vertexShader.build());
        gl.linkProgram(shaderProgram);

        //if creating shader program failed
        if(!gl.getProgramParameter(shaderProgram, gl.LINK_STATUS)){
          console.log("Unable to initialize the shader program: " + gl.getProgramInfoLog(shaderProgram));
        }

        gl.useProgram(shaderProgram);

        vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
        gl.enableVertexAttribArray(vertexPositionAttribute);
    }
    
    initBuffer(){
        console.log("Initialize buffers"); 
        squareVerticesBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, squareVerticesBuffer);
        var vertices = [0.5, 0.5, 0.0];
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
        gl.vertexAttribPointer(vertexPositionAttribute, 2, gl.FLOAT, false, 0, 0);
    }

    
    addVertices(vertices) {
        
    }
    
    addTextures(textureCoordinates) {
        
    }
    
    drawScene() {
        console.log("Drawing a scene");
        //set background
        gl.clearColor(0.0, 0.0, 0.0, 1.0);
        //enable depth testing
        gl.enable(gl.DEPTH_TEST);
        //near thing obscure far
        gl.depthFunc(gl.LEQUAL);
        //clear color and depth buffer
        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

        gl.drawArrays(gl.POINTS, 0, 1);
    }
}

class WebglUtils {
    static resizeCanvas(glCanvas) {
        
    }
}

//---------------------------------- Camera ----------------------------------
class CameraMovement {
    constructor() {
        
    }
}

//---------------------------------- Main function ----------------------------------
function main(canvas, path){
    console.log("Starting webgl viewer");
    
    var webgl = new Webgl(canvas);
  
    if(!gl){
      return;
    }
    
    webgl.initShaders();
    webgl.initBuffer();
    webgl.drawScene();
}