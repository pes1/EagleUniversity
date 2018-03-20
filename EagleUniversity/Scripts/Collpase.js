$(function () {
    $("tbody#firstlist").collapse('hide');
    $("tfoot#secondlist").collapse('hide');
});

$(function () {
    $("#Demo").collapse('hide');
});
$(function () {
    $("#CollapseDetail").click(function () {
        $("#Demo").collapse('toggle');       
    });
});

$(function () {
    $(".btn.btn-link").click(function () {
        var CurrentId = $(this).parent().parent().next().attr("id", "newId");
        $(CurrentId).collapse('toggle');      

    });
});