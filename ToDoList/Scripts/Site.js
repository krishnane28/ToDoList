$(document).ready(function () {
    // AJAX get request to generate create new task bootstrap modal
    $.ajax({
        url: "/ToDos/CreateModal",
        type: "GET",
        success: function (result) {
            $("#newTaskModal").html(result);
        }
    });
});

