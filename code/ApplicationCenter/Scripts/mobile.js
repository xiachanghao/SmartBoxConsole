function get_mobile_os() {
    var sUserAgent = window.navigator.userAgent;
    if (sUserAgent.indexOf('Android') != -1) {
        return 'Android';
    }
    if (sUserAgent.indexOf('iPhone') != -1) {
        return 'iPhone';
    }
}

function get_mobile_os_version() {
    var sUserAgent = window.navigator.userAgent;
    if (sUserAgent.indexOf('Android') != -1) {
        var arr = /Android[\s]*(\d\.\d(\.\d)?){1}/.exec(sUserAgent);
        if (arr && arr !== undefined && arr.length > 0) {
            return arr[1];
        }

        return '';
    } else if (sUserAgent.indexOf('iPhone') != -1) {
        var arr = /iPhone[\s]*OS[\s]*(\d\_\d(\_\d)?)/.exec(sUserAgent);
        if (arr && arr !== undefined && arr.length > 0) {
            return arr[1];
        }
        return '';
    }
    return '';
}