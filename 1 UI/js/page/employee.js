$(document).ready(function () {
    $(".employee-profile-dialog").hide();
    $(".dialog-modal").hide();
    // $(".combobox .combobox-data").hide();
})

$(".content .content-body .grid table tbody tr").click(function () {
    $(".employee-profile-dialog").show();
    $(".dialog-modal").show();
});

// // Sự kiện click vào button hiển thị ra form
// $(".button__adduser").click(function () {
//     $(".employee-profile-dialog").show();
//     $(".dialog-modal").show();
// });

$(".btn-cancel").click(function () {
    $(".employee-profile-dialog").hide();
    $(".dialog-modal").hide();
});

$(".btn-save").click(function () {
    $(".employee-profile-dialog").hide();
    $(".dialog-modal").hide();
});

$(".employee-profile-dialog .dialog-header button.btn-exit").click(function () {
    $(".employee-profile-dialog").hide();
    $(".dialog-modal").hide();
});




// $(".combobox button").click(function () {
//     $(".combobox .combobox-data").show();
// })
// $(".combobox .combobox-item").click(function () {
//     $(".combobox .combobox-data").hide();
// })