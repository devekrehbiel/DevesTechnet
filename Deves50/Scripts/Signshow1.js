var myximage1 = new Image();
var myximage2 = new Image();
var myximage3 = new Image();
var myximage4 = new Image();
var myximage5 = new Image();
var myximage6 = new Image();
var myximage7 = new Image();
var myximage8 = new Image();
var myximage9 = new Image();

var myximgsrc01 = "/Images/Signshow1/Header1a";
var myximgsrc02 = "/Images/Signshow1/Header1";
var myximgsrc03 = "/Images/Signshow1/Header1";
var myximgsrc04 = "/Images/Signshow1/Header1";
var myximgsrc05 = "/Images/Signshow1/Header1a";
var myximgsrc06 = "/Images/Signshow1/Header1a";
var myximgsrc07 = "/Images/Signshow1/Header1";
var myximgsrc08 = "/Images/Signshow1/Header1a";

var ssnumimgs = 8;

myximage1.src = myximgsrc01 + ".gif";
myximage2.src = myximgsrc02 + ".gif";
myximage3.src = myximgsrc03 + ".gif";
myximage4.src = myximgsrc04 + ".gif";
myximage5.src = myximgsrc05 + ".gif";
myximage6.src = myximgsrc06 + ".gif";
myximage7.src = myximgsrc07 + ".gif";
myximage8.src = myximgsrc08 + ".gif";

var ssstep = 1;

function slideityz() {
    if (!document.images) return;
    document.images.slidemex.src = eval("myximage" + ssstep + ".src");
    if (ssstep < ssnumimgs)
        ++ssstep;
    else
        ssstep = 1;
    setTimeout("slideityz()", 250)
}

slideityz();