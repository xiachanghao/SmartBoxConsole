function autosize_flexgrid(selector, _otherpm) {
    var maiheight = document.documentElement.clientHeight; // 减去边框和左边的宽度
    var otherpm = 152;
    if (_otherpm)
        otherpm = _otherpm;
    var gh = maiheight - otherpm;
    $(selector).flexResize('100%', gh + 'px');
}

function autosize(selector, _otherpm) {
    var maiheight = document.documentElement.clientHeight; // 减去边框和左边的宽度
    var otherpm = 152;
    if (_otherpm)
        otherpm = _otherpm;
    var gh = maiheight - otherpm;
    $(selector).css('width', '100%').height(gh + 'px');
}