var xmyimage1 = new Image();
var xmyimage2 = new Image();
var xmyimage3 = new Image();
var xmyimage4 = new Image();
var xmyimage5 = new Image();
var xmyimage6 = new Image();
var xmyimage7 = new Image();
var xmyimage8 = new Image();
var xmyimage9 = new Image();

var myimgsrc001 = "http://images.devestechnet.com/Signshow3/Open 24 hrs";
var myimgsrc002 = "http://images.devestechnet.com/Signshow3/Open 24 hrs";
var myimgsrc003 = "http://images.devestechnet.com/Signshow3/Open 24 hrs2";
var myimgsrc004 = "http://images.devestechnet.com/Signshow3/Open 24 hrs2";
var myimgsrc005 = "http://images.devestechnet.com/Signshow3/Open 24 hrs";
var myimgsrc006 = "http://images.devestechnet.com/Signshow3/Open 24 hrs24";
var myimgsrc007 = "http://images.devestechnet.com/Signshow3/Open 24 hrs24";
var myimgsrc008 = "http://images.devestechnet.com/Signshow3/Open 24 hrs";

var snumimgs = 8;

xmyimage1.src = myimgsrc001 + ".gif";
xmyimage2.src = myimgsrc002 + ".gif";
xmyimage3.src = myimgsrc003 + ".gif";
xmyimage4.src = myimgsrc004 + ".gif";
xmyimage5.src = myimgsrc005 + ".gif";
xmyimage6.src = myimgsrc006 + ".gif";
xmyimage7.src = myimgsrc007 + ".gif";
xmyimage8.src = myimgsrc008 + ".gif";

var ssstep = 1;

function slideitzz() {
    if (!document.images) return;
    document.images.slidemc.src = eval("xmyimage" + ssstep + ".src");
    if (ssstep < snumimgs)
        ++ssstep;
    else
        ssstep = 1;
    setTimeout("slideitzz()", 800)
}

slideitzz();