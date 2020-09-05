$(document).ready(function () {

    $('#btnEntry').click(function () {
        var k = $('#txtemail').val();
        var s = $('#txtpass').val();

        var usr = new All.Models.Vusercontol();

        usr.USERNAME = k;
        usr.PASS = s;

        var mdl = new All.Models.cRequest();
        mdl.data = JSON.stringify(usr);

        str = "";


        var settings = {
            "url": "https://localhost:5001/GateOfNewWorld/auth",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify(mdl),
        };

        $.ajax(settings).done(function (response) {
            console.log(response);
            str = response;
        });


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

});