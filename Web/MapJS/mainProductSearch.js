

function pfxURL() {
    pfxURL.staticVar;
}




function isNumberKey(evt, target) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var isnum = true;
    if (!evt.ctrlKey && charCode > 31 && (charCode < 48 || charCode > 57)) {
        isnum = false;
        if (evt.which)
            evt.preventDefault();
        else
            evt.returnValue = false;
    }
    return fireDefault(evt, target) && isnum;
}

function fireDefault(evt, target) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13 && target != undefined) {
        evt.cancelBubble = true;
        if (evt.stopPropagation) {
            evt.stopPropagation();
            evt.preventDefault();
        }
        var btn = document.getElementById(target);
        btn.click();
        return false;
    }
    return true;
}