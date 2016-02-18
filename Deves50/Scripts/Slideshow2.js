 
var imgSlide;
var pic = 0;
window.onload = function ()
{
 
    imgSlide = document.getElementById('img');
 
    // preload images
    images = new Array();
    images[0] = new Image();
    images[0].src = "http://images.devestechnet.com/Restorations/1950B/D50b101lg.jpg";
    images[1] = new Image();
    images[1].src = "http://images.devestechnet.com/Restorations/1950B/D50b102lg.jpg";
    images[2] = new Image();
    images[2].src = "http://images.devestechnet.com/Restorations/1950B/D50b103lg.jpg";
    images[3] = new Image();
    images[3].src = "http://images.devestechnet.com/Restorations/1950B/D50b104lg.jpg";
 
}
 
function slide()
{
    imgSlide.src = images[pic].src;
    if(pic < 3) // images.length - 1 can be used here
    {
        pic++;
    }
    else
    {
        pic = 0
 
    }
    timer = setTimeout(slide, 5000);
}
 
function prev()
{
    if(timer)
        stopSlide();
 
    if(pic == 0)
    {
 
        pic = 3;
 
        imgSlide.src = images[pic].src;
    }
    else
    {
        pic--;
        imgSlide.src = images[pic].src;
    }
}
 
function next()
{
    if(timer)
        stopSlide();
 
    if(pic == 3)
    {
 
        pic = 0;
        imgSlide.src = images[pic].src;
    }
    else
    {
        pic++;
        imgSlide.src = images[pic].src;
    }
 
 
}
 
function stopSlide()
{
    clearTimeout(timer);
}
 
 
