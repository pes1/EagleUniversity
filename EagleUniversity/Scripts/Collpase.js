$(document).ready(function () {    
    $("tbody#firstlist").addClass('collapse');
    $("tr#secondlist").addClass('collapse');

    var redirectLink = $("#Redirected").text();

    if (redirectLink.length>10) {
        var redirectModule = "." + redirectLink.substring(redirectLink.search("module"));
        var redirectActivity = "." + redirectLink.substring(0, redirectLink.search("module") - 1);
        $(redirectModule).removeClass('collapse');
        $(redirectModule).addClass('in');       
        $(redirectActivity).addClass('in');
    }

    if (redirectLink != "Default") {
        BackID = "." + redirectLink;
        $(BackID).addClass('in');
    }
    if (redirectLink === "Document") {
        $("#Default").removeClass("active");
        $("#home").removeClass("in active");
        $("#Student").removeClass("active");
        $("#menu1").removeClass("in active");
        $("#Document").addClass("active");
        $("#menu2").addClass("in active");
    }
    else if (redirectLink === "Student") {
        $("#Default").removeClass("active");
        $("#home").removeClass("in active");
        $("#Document").removeClass("active");
        $("#menu2").removeClass("in active");
        $("#Student").addClass("active");
        $("#menu1").addClass("in active");
    }
});
$(function () {
    $(".btn.btn-link.col-lg-1").click(function () {
        var CurrentId = $(this).parent().next().attr("id", "newId");        
        $(CurrentId).collapse('toggle');
    });
});
$(function () {
    $(".btn.btn-link").click(function () {
        var CurrentId = $(this).parent().parent().next().attr("id", "newId");        
        $(CurrentId).collapse('toggle');      

    });
});


