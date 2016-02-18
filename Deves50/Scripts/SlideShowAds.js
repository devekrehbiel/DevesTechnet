var adimage1 = new Image();
var adimage2 = new Image();
var adimage3 = new Image();
var adimage4 = new Image();
var adimage5 = new Image();
var adimage6 = new Image();
var adimage7 = new Image();
var adimage8 = new Image();

var myimgsrc1 = "http://images.devestechnet.com/Advertisements/JimCarterAd";
var myimgsrc2 = "http://images.devestechnet.com/Advertisements/TimsLogo";
var myimgsrc3 = "http://images.devestechnet.com/Advertisements/DevesTechNeta";
var myimgsrc4 = "http://images.devestechnet.com/Advertisements/DavesBlog";
var myimgsrc5 = "http://images.devestechnet.com/Advertisements/ChevyTalk";
var myimgsrc6 = "http://images.devestechnet.com/Advertisements/KeithHardy";
var myimgsrc7 = "http://images.devestechnet.com/Advertisements/BradAllen";

var numimgx = 7;

adimage1.src = myimgsrc1 + ".gif";
adimage2.src = myimgsrc2 + ".gif";
adimage3.src = myimgsrc3 + ".gif";
adimage4.src = myimgsrc4 + ".gif";
adimage5.src = myimgsrc5 + ".gif";
adimage6.src = myimgsrc6 + ".gif";
adimage7.src = myimgsrc7 + ".gif";

var astep = 1;

function slider() {
    if (!document.images) return;
    document.images.slider.src = eval("adimage" + astep + ".src");
    if (astep < numimgx)
        ++astep;
    else
        astep = 1;
    setTimeout("slider()", 16000)
}

slider();