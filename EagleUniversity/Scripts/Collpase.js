$(document).ready(function () {
    $("tbody#firstlist").addClass("collapse");
    $("tr#secondlist").addClass("collapse"); 
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
