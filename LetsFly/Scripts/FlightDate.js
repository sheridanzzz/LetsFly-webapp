$(document).ready(function () {
    $('.datepicker').datepicker
    ({
        format: "dd/mm/yyyy",
        orientation: "bottom",
        startDate: new Date()
    }).datepicker(
        "setDate", new Date()
    );
});