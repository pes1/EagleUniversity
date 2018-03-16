$("h2").on({
    mouseenter: function () {
        $(this).css("background-color", "lightgray");
    },
    mouseleave: function () {
        $(this).css("background-color", "lightblue");
    },
    click: function () {
        $(this).css("background-color", "yellow");
    }
});
//if (window.location.hash) {
//    var hash = window.location.hash;
//    alert(hash)
//    var test = $(hash).offset().top - 160;
//    $('body,html').animate({ scrollTop: test }, 1500);
//};



function OnComplete(request, status) {
        location.reload();
    }

    function OnBegin() {
        $(window).scrollTop(0);
    }
