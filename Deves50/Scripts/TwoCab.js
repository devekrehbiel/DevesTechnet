
$(function () {
    setupTheSlideShowa();
    setupButtonsa();
});

var setupButtonsa = function () {
    $('#preva').on('click', function () {
        goPreviousa();
    });
    $('#nexta').on('click', function () {
        goNexta();
    });
    $('#stopTheShowa').on('click', function () {
        if ($('#slideShowGoa').val() == "true") {
            $(this).html('Hold the Show!');
            stopIta();
        } else {
            $(this).html('Start the Show!');
            makeItGoa();
        }
    });
    $('#useMaina').on('click', function () {
        $('#MethodNamea').val("GetImagesFromTwoCabDirectory");
        $('#slideShowImgIda').val(0);
        goNexta();
    });
    $('#useOthera').on('click', function () {
        $('#MethodNamea').val("GetImagesFromOtherTwoCabDirectory");
        $('#slideShowImgIda').val(0);
        goNexta();
    });

}

var setupTheSlideShowa = function () {
    autoRotatea(true);
}

var goNexta = function () {
    var slideShowImgIda = $('#slideShowImgIda').val();
    ++slideShowImgIda;
    switchSlidesa(slideShowImgIda);
}

var goPreviousa = function () {
    var slideShowImgIda = $('#slideShowImgIda').val();
    --slideShowImgIda;
    switchSlidesa(slideShowImgIda);
}

var autoRotatea = function (start) {
    var shouldRotatea = $('#slideShowGoa').val();
    console.log(shouldRotatea);
    if (shouldRotatea == "false" && !start) {
        goNexta();
    }
    setTimeout("autoRotatea()", 5000); // milliseconds
}

var switchSlidesa = function (myimgId) {
    var methoda = $('#MethodNamea').val();
    $('#slideShowa').hide();
    $('#ajaxLoada').show();
    $.get("/Home/" + methoda + "?myimgId=" + myimgId).done(function (response) {
        console.log(response); // this will show us the response
        console.log("This is my image path:" + response["imgPath"])
        $('#slideShowa').attr('src', "/" + response["imgPath"]); // this is probably wrong gotta tweak it a little
        $('#slideShowImgIda').val(response["imgId"]);
        $('#ajaxLoada').hide();
        $('#slideShowa').show();
    }).fail(function (error) {
        console.log(error);
    });
}

var stopIta = function () {
    $('#slideShowGoa').val('false');
}

var makeItGoa = function () {
    $('#slideShowGoa').val('true');
}