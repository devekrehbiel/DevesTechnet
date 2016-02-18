
$(function () {
    setupTheSlideShow();
    setupButtons();
});

var setupButtons = function () {
    $('#prev').on('click', function () {
        goPrevious();
    });
    $('#next').on('click', function () {
        goNext();
    });
    $('#stopTheShow').on('click', function () {
        if ($('#slideShowGo').val() == "true") {
            $(this).html('Hold the Show!');
            stopIt();
        } else {
            $(this).html('Start the Show!');
            makeItGo();
        }
    });
    $('#useMain').on('click', function () {
        $('#MethodName').val("GetImagesFromTuneUpDirectory");
        $('#slideShowImgId').val(0);
        goNext();
    });
    $('#useOther').on('click', function () {
        $('#MethodName').val("GetImagesFromOtherTuneUpDirectory");
        $('#slideShowImgId').val(0);
        goNext();
    });

}

var setupTheSlideShow = function () {
    autoRotate(true);
}

var goNext = function () {
    var slideShowImgId = $('#slideShowImgId').val();
    ++slideShowImgId;
    switchSlides(slideShowImgId);
}

var goPrevious = function () {
    var slideShowImgId = $('#slideShowImgId').val();
    --slideShowImgId;
    switchSlides(slideShowImgId);
}

var autoRotate = function (start) {
    var shouldRotate = $('#slideShowGo').val();
    console.log(shouldRotate);
    if (shouldRotate == "false" && !start) {
        goNext();
    }
    setTimeout("autoRotate()", 5000); // milliseconds
}

var switchSlides = function (imgId) {
    var method = $('#MethodName').val();
    $('#slideShow').hide();
    $('#ajaxLoad').show();
    $.get("/Home/" + method + "?imgId=" + imgId).done(function (response) {
        console.log(response); // this will show us the response
        console.log("This is my image path:" + response["imgPath"])
        $('#slideShow').attr('src', "/" + response["imgPath"]); // this is probably wrong gotta tweak it a little
        $('#slideShowImgId').val(response["imgId"]);
        $('#ajaxLoad').hide();
        $('#slideShow').show();
    }).fail(function (error) {
        console.log(error);
    });
}

var stopIt = function () {
    $('#slideShowGo').val('false');
}

var makeItGo = function () {
    $('#slideShowGo').val('true');
}