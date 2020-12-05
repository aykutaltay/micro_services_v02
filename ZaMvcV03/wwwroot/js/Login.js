$(document).ready(function () {


    $('#btnEntry').click(function () {
        var k = $('#txtemail').val();
        var s = $('#txtpass').val();

        var usr = new All.Models.Vusercontol();

        usr.USERNAME = k;
        usr.PASSWORD = md5(s);

        var mdl = new All.Models.cRequest();
        var result = new All.Models.cResponse();


        grecaptcha.ready(function () {
            grecaptcha.execute('6LcUT8kZAAAAADHqHET4v2sJQr8Ozbta4Ti9s5dj', { action: 'submit' })
                .then(function (token) {

                    mdl.token = token;
                    mdl.data = JSON.stringify(usr);


                    var tmp = JSON.stringify(mdl);

                    var settings = {
                        "url": All.webpageoptauth,
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify(mdl),
                    };

                    $.ajax(settings).done(function (response) {
                        result = response;
                        if (result.message_code == 1) {
                            localStorage.setItem("token", result.token);
                            var ss = All.webpageurl + All.webpageMainIndex;
                            window.location.href = ss;
                        }
                        else {
                            $("div.warning").html(response.message);
                            $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
                        }
                    });
                });
        });


    });


    $('#btnNewEntry').click(function () {
        var k = $('#txtNewemail').val();
        var a = $('#txtNewusername').val();
        var s = $('#txtNewpass').val();

        var usr = new All.Models.VNusercontol();
        var result = new All.Models.cResponse();

        usr.USERNAME = k;
        usr.PASSWORD = md5(s);
        usr.NAMESURNAME = a;

        var mdl = new All.Models.cRequest();
        mdl.data = JSON.stringify(usr);



        //kontroller
        if (All.Methods.ValidateEmail(k) == false)
            return;

        grecaptcha.ready(function () {
            grecaptcha.execute('6LcUT8kZAAAAADHqHET4v2sJQr8Ozbta4Ti9s5dj', { action: 'submit' })
                .then(function (token) {

                    mdl.token = token;
                    mdl.data = JSON.stringify(usr);

                    var settings = {
                        "url": All.webpaceoptnuser,
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify(mdl),
                    };

                    $.ajax(settings).done(function (response) {
                        $("div.warning").html(response.message);
                        $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);

                    });
                });
        });

    });

    $('#btnResetPass').click(function () {
        var mail = $('#txtResetMail').val();

        var mdl = new All.Models.cRequest();
        mdl.data = mail;


        var settings = {
            "url": All.weppageoptforgetpass,
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(mdl),
        };

        $.ajax(settings).done(function (response) {
            result = response;
            //gelen response bilgisi
            $("div.warning").html(response.message);
            $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
        });
    });

});