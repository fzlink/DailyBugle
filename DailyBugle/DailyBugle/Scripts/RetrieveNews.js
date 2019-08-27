$(document).ready(function () {
    var apiBaseUrl = "http://localhost:53786/";
    $("#btnGetData").click(function () {
        $.ajax({
            url: apiBaseUrl + 'api/TestApi',
            type: "GET",
            dataType: "json",
            success: function (data) {
                var $table = $('<table/>').addClass('dataTable table table-bordered table-striped');
                var $header = $('<thead/>').html('<tr><th>Title</th><th>AuthorName</th><th>Category</th></tr>');
                $table.append($header);
                $.each(data, function (_i,val) {
                    var $row = $('<tr/>');
                    $row.append($('<td/>').html(val.Title));
                    $row.append($('<td/>').html(val.AuthorName));
                    $row.append($('<td/>').html(val.Category));
                    $table.append($row);
                });
                $("#updatePanel").html($table);
            },
            error: function () {
                alert("Error");
            }
        });
    });
});