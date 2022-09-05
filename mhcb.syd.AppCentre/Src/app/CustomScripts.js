function selectElementContents(el) {
    var body = document.body, range, sel;

    if (document.createRange && window.getSelection) {
        range = document.createRange();
        sel = window.getSelection();
        sel.removeAllRanges();
        range.selectNodeContents(el);
        sel.addRange(range);
    }
    document.execCommand("Copy");
}


// Function to hide the Spinner
function hideSpinner() {
    document.getElementById('spinner')
        .style.display = 'none';
}

function showSpinner() {
    //document.getElementById('spinner').removeAttr("style");
    document.getElementById('spinner').style.removeProperty("display");

}


