Number.prototype.formatMoney = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return '$' + this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};

Number.prototype.formatDecimals = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};

function EnterKeyFilter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function ShowContentArea() {
    $('#mainContentArea').show();
    $('#mainContentLoading').hide();
}

function ShowLoading() {
    $('#mainContentArea').hide();
    $('#mainContentLoading').show();
}