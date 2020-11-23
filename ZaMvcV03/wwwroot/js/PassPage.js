var SavePas = function () {
    var mdl = new All.Models.cRequest();
    mdl.token = localStorage.getItem("token");

    var ps = document.getElementById('txtpass').value;
    var ps_rp = document.getElementById('txtpassrepeat').value;

    if (ps == ps_rp) {
        mdl.data = ps;

        var settings = {
            "url": All.webpageoptsavepass,
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + mdl.token
            },
            "data": JSON.stringify(mdl),
            "async": "false"
        };

        $.ajax(settings).done(function (response) {
            if (response.message_code == 1) {

            }
            else {
                $("div.warning").html(response.message);
                $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
            }
        });


    }
    else {
        $("div.warning").html('Girilen bilgiler eşit değil');
        $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
    };

};

$(document).ready(function () {
    $('#PassbtnSave').on('click', function () {
        SavePas();
    });
});