
var canvas = document.querySelector('canvas');
var c = canvas.getContext('2d');

let res = 15;
let width = 60;
let height = 60;

let arr;
let arr2;
let speed = 100;

initial();

function initial() {
    arr = new Array(width);
    for (var i = 0; i < arr.length; i++) {
        arr[i] = new Array(height);
    }

    arr2 = new Array(width);
    for (var i = 0; i < arr.length; i++) {
        arr2[i] = new Array(height);
    }
    arrRandom(arr);
}

// set initial board
function arrRandom(arr) {
    for (var i = 0; i < width; i++) {
        for (var j = 0; j < height; j++) {
            arr2[i][j] = 0;
            if (i == 0 || j == 0) {
                arr[i][j] = 0;
            }
            else {
                arr[i][j] = Math.floor(Math.random() * 2);
            }
        }
    }
    drawCanvas(arr);
    // initial pause
    setTimeout(function() {
        gameBoard(arr);
    }, 1000);
}

// check neighbor
function cellCondition(arr, i, j) {
    let neighbor = 0;
    for (var m = -1; m < 2; m++) {
        for (var n = -1; n < 2; n++) {
            if (arr[i + m][j + n] == 1) {
                neighbor++;
            }
        }
    }
    neighbor -= arr[i][j];
    return neighbor;
}

function drawCanvas(arr) {
    for (var i = 0; i < width; i++) {
        for (var j = 0; j < height; j++) {
            if (arr[i][j] == 1) {
                c.fillStyle = "black";
                c.fillRect(j * res, i * res, res, res);
            }
            else {
                c.fillStyle = "white";
                c.fillRect(j * res, i * res, res, res);
            }
        }
    }
}

function gameBoard(arr) {
    let slider = document.getElementById('myRange');
    let pauseBtn = document.getElementsByClassName('btn-pause')[0];
    slider.addEventListener('click', changeSpeed);
    pauseBtn.addEventListener("click",stopBoard);
    
    for (var i = 1; i < width - 1; i++) {
        for (var j = 1; j < height - 1; j++) {
            if (cellCondition(arr, i, j) < 2 || cellCondition(arr, i, j) > 3) {
                arr2[i][j] = 0;
            }
            else if (cellCondition(arr, i, j) == 3 && arr[i][j] == 0) {
                arr2[i][j] = 1;
            }
            else if ((cellCondition(arr, i, j) == 3 || cellCondition(arr, i, j) == 2) && arr2[i][j] == 1) {
                arr2[i][j] = 1;
            }
        }
    }
    drawCanvas(arr2);
    setTimeout(function () {
        gameBoard(arr2);
    }, speed);

    function stopBoard() {
        speed = 2147483647;
        console.log("pause")
    }
    
    function changeSpeed() {
        var value = slider.value;
        speed = 1000 - (value *  10);
        console.log(speed)
    }

    function reset() {

    }
}
