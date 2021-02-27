$(document).ready(function () {
    $('.datepicker').datepicker({
        format: "dd/mm/yyyy",
        orientation: "bottom"

    }).datepicker(
        "setDate", new Date()
    );
});

