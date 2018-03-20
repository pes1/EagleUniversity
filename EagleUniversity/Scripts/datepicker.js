$(document).ready(function () {
    $(document).on('focus', '#Date', function () {
        $(this).datetimepicker({
            locale: '@System.Configuration.ConfigurationManager.AppSettings["UICulture"]',
            format: 'YYYY-MM-DD',
            weekStart: 1,
            open: function () { $('.k-weekend a').bind('click', function () { return false; }); }
        });
    });
});