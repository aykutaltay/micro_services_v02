// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//açıklamayı gizle
$("div.warning").fadeOut(1);

var GetRetoken = function () {
    var mdl = new All.Models.cRequest();
    mdl.token = localStorage.getItem("token");
    mdl.data = "";

    var settings = {
        "url": All.webpageoptretoken,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + mdl.token
        },
        "data": JSON.stringify(mdl),
    };

    $.ajax(settings).done(function (response) {
        if (response.message_code == 1) {
            localStorage.setItem("token", response.token);
           
        }
    });
};
var GetTime01 = function () {
    setTimeout(GetRetoken, 303000);
    setTimeout(GetTime02, 305000);
}
var GetTime02 = function () {
    setTimeout(GetRetoken, 303000);
    setTimeout(GetTime01, 305000);
}