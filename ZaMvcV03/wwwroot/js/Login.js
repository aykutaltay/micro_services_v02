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

                    var settings = {
                        "url": All.srv+"/GateOfNewWorld/auth",
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify(mdl),
                    };

                    $.ajax(settings).done(function (response) {
                        //console.log(response);
                        result = JSON.parse(response);
                        alert(result.message);
                    });
                });
        });



        //kontroller
        //if (All.Methods.ValidateEmail(k) == false)
        //    return;





        //All.postJSON('https://localhost:5001/GateOfNewWorld/auth', JSON.stringify(mdl), function (res) {
        //    try {
        //        str = res;
        //        //var resultModel = JSON.parse(res);
        //        //localStorage.setItem('User', res);
        //        //location.href = '/Default/Index';
        //        //$('#msg').removeClass('hidden').html('Giriş Başarılı');
        //        localStorage.setItem('User', res);
        //        localStorage.setItem('User001', str);
        //    } catch (e) {
        //        //$('#frmLogin').show();
        //        //$('#msg').removeClass('hidden').html(e);
        //        str = html(e);
        //    }
        //});

        //console.log(str);


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
                        "url": All.srv + "/GateOfNewWorld/nuser",
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify(mdl),
                    };

                    $.ajax(settings).done(function (response) {
                        //console.log(response);
                        //result = JSON.parse(response);
                        //alert(response.message);
                        //$('#a_message').removeClass('hidden').html(response.message);
                        $("div.warning").html(response.message);
                        $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
                        
                    });
                });
        });

    });

});