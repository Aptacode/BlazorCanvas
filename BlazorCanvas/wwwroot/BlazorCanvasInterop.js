//Canvas
var canvasElement;
var ctx;

function registerCanvas(element) {
    canvasElement = element;
    ctx = canvasElement.getContext("2d");
}

//Helpers
function toFloatArray(input) {
    const m = input + 12;
    const r = Module.HEAP32[m >> 2];
    return new Float32Array(Module.HEAPF32.buffer, m + 4, r);
}

//Api
function clearRect(params) {
    const dimensions = toFloatArray(params);
    ctx.clearRect(dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
}

function fillRect(params) {
    const dimensions = toFloatArray(params);
    ctx.fillRect(dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
}

function strokeRect(params) {
    const dimensions = toFloatArray(params);
    ctx.strokeRect(dimensions[0], dimensions[1], dimensions[2], dimensions[3]);
}

function ellipse(params) {
    const dimensions = toFloatArray(params);
    ctx.ellipse(dimensions[0], dimensions[1], dimensions[2], dimensions[3], dimensions[4], dimensions[5], dimensions[6]);
}

function fill() {
    ctx.fill();
}

function stroke() {
    ctx.stroke();
}

function beginPath() {
    ctx.beginPath();
}

function closePath() {
    ctx.closePath();
}

function moveTo(params) {
    const dimensions = toFloatArray(params);
    ctx.moveTo(dimensions[0], dimensions[1]);
}

function lineTo(params) {
    const dimensions = toFloatArray(params);
    ctx.lineTo(dimensions[0], dimensions[1]);
}

function polyline(params) {
    const vertices = toFloatArray(params);
    ctx.beginPath();

    ctx.moveTo(vertices[0], vertices[1]);
    for (i = 2; i < vertices.length; i += 2) {
        ctx.lineTo(vertices[i], vertices[i+1]);
    }
}

function polygon(params) {
    const vertices = toFloatArray(params);
    ctx.beginPath();

    ctx.moveTo(vertices[0], vertices[1]);
    for (i = 2; i < vertices.length; i += 2) {
        ctx.lineTo(vertices[i], vertices[i + 1]);
    }
    ctx.closePath();
}

function fillStyle(param) {
    const style = BINDING.conv_string(param);
    ctx.fillStyle = style;
}

function strokeStyle(param) {
    const style = BINDING.conv_string(param);
    ctx.strokeStyle = style;
}

function lineWidth(param) {
    const dimensions = toFloatArray(param);
    ctx.lineWidth = dimensions[0];
}

function textAlign(param) {
    const alignment = BINDING.conv_string(param);
    ctx.textAlign = alignment;
}

function font(text) {
    const content = BINDING.conv_string(text);
    ctx.font = content;
}

function fillText(text, params) {
    const content = BINDING.conv_string(text);
    const dimensions = toFloatArray(params);
    ctx.fillText(content, dimensions[0], dimensions[1]);
}

function wrapText(text, params) {
    var words = BINDING.conv_string(text).split(' ');
    const dimensions = toFloatArray(params);

    var line = '';
    var x = dimensions[0];
    var y = dimensions[1];
    var maxWidth = dimensions[2];
    var maxHeight = dimensions[3];
    var lineHeight = dimensions[4];

    var lines = [];
    var hasOverflow = false;
    for (var n = 0; n < words.length; n++) {
        var testLine = line + words[n] + ' ';
        var metrics = ctx.measureText(testLine);
        var testWidth = metrics.width;
        if (testWidth > maxWidth && n > 0) {
            lines.push(line);
            line = words[n] + ' ';
            y += lineHeight;
        } else {
            line = testLine;
        }

        if (y - dimensions[1] > maxHeight - lineHeight) {
            hasOverflow = true;
            break;
        }
    }

    if (!hasOverflow) {
        lines.push(line);
    }

    var y = dimensions[1] - (lines.length / 2);
    for (var n = 0; n < lines.length; n++) {
        ctx.fillText(lines[n], x, y+=lineHeight);
    }
}