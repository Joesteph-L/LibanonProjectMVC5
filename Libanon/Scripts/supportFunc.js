var loadFileImg = function (event, imgId) {
    var output = document.getElementById(imgId);
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)
    }
};