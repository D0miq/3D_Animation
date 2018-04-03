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
            var allFaces = new Int32Array(getContent(facesLength * 4 * 6));
            var j = 0;
            this.faces = [];
            this.textureIndex = [];
            for(var i = 0; i < allFaces.length; i += 2) {
                this.faces[j] = allFaces[i] - 1;
                this.textureIndex[j] = allFaces[i + 1] - 1;
                j++;
            }
        };

        this.readTextureCoordinates = function () {
            var texturesLength = new Int32Array(getContent(4))[0];
            console.log("Textures length", texturesLength);
            this.textureCoordinates = texturesLength == 0 ? null : new Float32Array(getContent(texturesLength * 4));
        };
    }

    readFile() {
        var averageTrajectory = this.readAverageTrajectory();
        var eigenVectors = this.readEigenVectors();
        console.log(eigenVectors);
        var controlTrajectories = this.readControlTrajectories();
        console.log(controlTrajectories);
        this.readFaces();
        this.readTextureCoordinates();
        
        console.log("Compute vertices from matrices");
        var s = math.multiply(eigenVectors, controlTrajectories);
        var b = s.map(function (value, index, matrix) {
            return value + averageTrajectory[index[0]];
        }).toArray();

        console.log("Transform coordinates of vertices");
        this.vertices = [];
        for(var i = 0; i < b.length; i += 3) {
            var frame = [];
            for(var j = 0; j < b[i].length * 3; j += 3) {
                frame[j] = b[i][j / 3];
                frame[j + 1] = b[i + 1][j / 3];
                frame[j + 2] = b[i + 2][j / 3];
            }

            this.vertices[i / 3] = frame;
        }
   }
}

class Displacement {
    constructor(texture, newVertex) {
        this.texture = texture;
        this.newVertex = newVertex;
    }
};

class Vertices {    
    constructor(vertices) {
        this.vertices = vertices;
    }
    
    computeNormals(faces) {
        console.log("Computing normals");
        this.normals = [];
        for (var i = 0; i < this.vertices.length; i++) {
            console.log("Normals for " + (i + 1) + ". frame");
            this.normals[i] = [];
            for(var j = 0; j < this.vertices[0].length; j++) {
                this.normals[i][j] = 0;
            }
            
            for(var j = 0; j < faces.length; j += 3) {
                var A = [this.vertices[i][faces[j] * 3], this.vertices[i][faces[j] * 3 + 1], this.vertices[i][faces[j] * 3 + 2]];
                var B = [this.vertices[i][faces[j + 1] * 3], this.vertices[i][faces[j + 1] * 3 + 1], this.vertices[i][faces[j + 1] * 3 + 2]];
                var C = [this.vertices[i][faces[j + 2] * 3], this.vertices[i][faces[j + 2] * 3 + 1], this.vertices[i][faces[j + 2] * 3 + 2]];
                var normal = math.cross([B[0] - A[0], B[1] - A[1], B[2] - A[2]], [C[0] - A[0], C[1] - A[1], C[2] - A[2]]);
                this.normals[i][faces[j] * 3] += normal[0];
                this.normals[i][faces[j] * 3 + 1] += normal[1];
                this.normals[i][faces[j] * 3 + 2] += normal[2];
                this.normals[i][faces[j + 1] * 3] += normal[0];
                this.normals[i][faces[j + 1] * 3 + 1] += normal[1];
                this.normals[i][faces[j + 1] * 3 + 2] += normal[2];
                this.normals[i][faces[j + 2] * 3] += normal[0];
                this.normals[i][faces[j + 2] * 3 + 1] += normal[1];
                this.normals[i][faces[j + 2] * 3 + 2] += normal[2];
            }
        }
    }
    
    computeTextures(textureCoords, textureIndexes, faces) {
        this.textureCoords = [];
        
        if(!textureCoords || !textureIndexes) {     
            for (var i = 0; i < this.vertices[0].length / 3 * 2; i++) {
                this.textureCoords[i] = 0;
            }
            
            return;
        }
        
        var texturesList = [];
        for(var i = 0; i < this.vertices[0].length; i++) {
            texturesList[i] = [];
        }
        
        for(var i = 0; i < faces.length; i++) {
            var vertexIndex = faces[i];
            var textureIndex = textureIndexes[i];
            
            var contains = false;
            texturesList[vertexIndex].forEach(function(displ) {
                if (displ.texture == textureIndex) {
                    contains = true;
                    if (displ.newVertex != vertexIndex){
                        // nahraď v face
                        faces[i] = displ.newVertex;
                    }
                }
            });
            
            if (!contains) {
                if (texturesList[vertexIndex].length != 0) {
                    var newIndex = this.vertices[0].length / 3;
                    texturesList[vertexIndex][texturesList[vertexIndex].length] = new Displacement(textureIndex, newIndex);
                    // nahraď v face a přenes do všech framů animace a přidej textovací souřadnice
                    for(var j = 0; j < this.vertices.length; j++) {
                        this.vertices[j][newIndex * 3] = this.vertices[j][vertexIndex * 3];
                        this.vertices[j][newIndex * 3 + 1] = this.vertices[j][vertexIndex * 3 + 1];
                        this.vertices[j][newIndex * 3 + 2] = this.vertices[j][vertexIndex * 3 + 2];
                        this.normals[j][newIndex * 3] = this.normals[j][vertexIndex * 3];
                        this.normals[j][newIndex * 3 + 1] = this.normals[j][vertexIndex * 3 + 1];
                        this.normals[j][newIndex * 3 + 2] = this.normals[j][vertexIndex * 3 + 2];
                    }
                    
                    this.textureCoords[newIndex * 2] = textureCoords[textureIndex * 2];
                    this.textureCoords[newIndex * 2 + 1] = textureCoords[textureIndex * 2 + 1];
                    faces[i] = newIndex;
                } else {
                    texturesList[vertexIndex][texturesList[vertexIndex].length] = new Displacement(textureIndex, vertexIndex);
                    this.textureCoords[vertexIndex * 2] = textureCoords[textureIndex * 2];
                    this.textureCoords[vertexIndex * 2 + 1] = textureCoords[textureIndex * 2 + 1];
                }
            }
        }
    }
}

self.addEventListener('message', function(e) {
    console.log("Downloading file", e.data, "from the server");
    importScripts('https://cdnjs.cloudflare.com/ajax/libs/mathjs/4.0.1/math.min.js');             
    var request = new XMLHttpRequest();
    request.open("GET", e.data);
    request.responseType = "arraybuffer";
    request.onload = function (e) {
        var file = request.response;
        console.log(file);

        if (file) {
            var fileReader = new FileReader3BA(file);
            fileReader.readFile();

            var vertices = new Vertices(fileReader.vertices);
            vertices.computeNormals(fileReader.faces);
            vertices.computeTextures(fileReader.textureCoordinates, fileReader.textureIndex, fileReader.faces);
            self.postMessage([vertices, fileReader.faces]);
        } else {
            alert("Unable to read file " + this.path);
        }
    };

    request.send();
}, false);


