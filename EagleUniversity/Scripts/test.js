//$("h2").on({
//    mouseenter: function () {
//        $(this).css("background-color", "lightgray");
//    },
//    mouseleave: function () {
//        $(this).css("background-color", "lightblue");
//    },
//    click: function () {
//        $(this).css("background-color", "yellow");
//    }
//});

$(document).ready(
    function () {
       
        if ($("#Redirected").text() == "Document")
        {
            $("#Default").removeClass("active");
            $("#home").removeClass("in active");
            $("#Document").addClass("active");
            $("#menu2").addClass("in active");
        }
        else {
        }
        $("#Redirected").hide();
  
    }
);



function OnComplete(request, status) {
        location.reload();
    }

function OnBegin() {
        $(window).scrollTop(0);
    }
