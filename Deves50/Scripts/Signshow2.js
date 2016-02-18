var myimage1 = new Image();
var myimage2 = new Image();
var myimage3 = new Image();
var myimage4 = new Image();
var myimage5 = new Image();
var myimage6 = new Image();
var myimage7 = new Image();
var myimage8 = new Image();
var myimage9 = new Image();

var myimgsrc01 = "http://images.devestechnet.com/Signshow2/Header2a";
var myimgsrc02 = "http://images.devestechnet.com/Signshow2/Header2a";
var myimgsrc03 = "http://images.devestechnet.com/Signshow2/Header2";
var myimgsrc04 = "http://images.devestechnet.com/Signshow2/Header2a";
var myimgsrc05 = "http://images.devestechnet.com/Signshow2/Header2";
var myimgsrc06 = "http://images.devestechnet.com/Signshow2/Header2";
var myimgsrc07 = "http://images.devestechnet.com/Signshow2/Header2a";
var myimgsrc08 = "http://images.devestechnet.com/Signshow2/Header2a";

var snumimgs = 8;

myimage1.src = myimgsrc01 + ".gif";
myimage2.src = myimgsrc02 + ".gif";
myimage3.src = myimgsrc03 + ".gif";
myimage4.src = myimgsrc04 + ".gif";
myimage5.src = myimgsrc05 + ".gif";
myimage6.src = myimgsrc06 + ".gif";
myimage7.src = myimgsrc07 + ".gif";
myimage8.src = myimgsrc08 + ".gif";

var sstep = 1;

function slideitz() {
    if (!document.images) return;
    document.images.slideme.src = eval("myimage" + sstep + ".src");
    if (sstep < snumimgs)
        ++sstep;
    else
        sstep = 1;
    setTimeout("slideitz()", 500)
}

slideitz();