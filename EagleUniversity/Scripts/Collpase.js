$(document).ready(function () {    
    $("tbody#firstlist").addClass('collapse');
    $("tr#secondlist").addClass('collapse');

    if ($("#Redirected").text() != "Default") {
        BackID = "."+$("#Redirected").text();
        $(BackID).addClass('in');
    }
    if ($("#Redirected").text() === "Document") {
        $("#Default").removeClass("active");
        $("#home").removeClass("in active");
        $("#Student").removeClass("active");
        $("#menu1").removeClass("in active");
        $("#Document").addClass("active");
        $("#menu2").addClass("in active");
    }
    else if ($("#Redirected").text() === "Student") {
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


