var dt = null;
var result = new All.Models.cResponse();
var data_usrlist = new All.Models.Vmainuserlist();
var e_usr = new All.Models.Vgetuser();
var mainstat = 0;

var Getlist = function () {
    if (dt == null) {
        dt = $('#example').DataTable({
        });
    }

    dt.destroy();

    var mdl = new All.Models.cRequest();
    mdl.token = localStorage.getItem("token");
    mdl.data = "";

    var settings = {
        "url": All.webpaceoptmainuserList,
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
            data_usrlist = JSON.parse(response.data);
            dt = $('#example').DataTable({
                'data': data_usrlist,
                'destroy': true,
                'pagingType': 'simple_numbers',
                'columns': [
                    {
                        'data': 'Id',
                        'render': function (data, type, row, meta) {
                            return data;
                        }
                    },
                    {
                        'data': 'Durum',
                        'render': function (data, type, row, meta) {
                            return data;
                        }
                    },
                    {
                        'data': 'Yetki',
                        'render': function (data, type, row, meta) {
                            return data;
                        }
                    },
                    {
                        'data': 'Kod',
                        'render': function (data, type, row, meta) {
                            return data;
                        }
                    },
                    {
                        'data': 'Email',
                        'render': function (data, type, row, meta) {
                            return data;
                        }
                    },
                    {
                        'data': 'SonTarih',
                        'render': function (data, type, row, meta) {
                            return data;
                        }
                    }
                ]
            });

        }
        else {
            $("div.warning").html(response.message);
            $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
            var ss = All.webpageurl + All.webpageMainIndex;
            window.location.href = ss;
            return;
        }
    });

};
var Getuser = function () {
    var mdl = new All.Models.cRequest();
    mdl.token = localStorage.getItem("token");
    mdl.data = localStorage.getItem(All.String.strUserIDBilgi);

    var settings = {
        "url": All.wabpageoptmainuserget,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + mdl.token
        },
        "data": JSON.stringify(mdl),
        "async":"false"
    };


    $.ajax(settings).done(function (response) {
        if (response.message_code == 1) {
            e_usr = JSON.parse(response.data);
            Getuserpage();
        }
        else {
            $("div.warning").html(response.message);
            $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
        }
    });

};
var Getuserpage = function () {
    document.getElementById('txtusername').value = e_usr.userName;
    document.getElementById('txtuseremail').value = e_usr.userMail;
    document.getElementById('txtuseremail').disabled = true;
    document.getElementById('txtuserbackupemail').value = e_usr.userBackupMail;
    if (e_usr.statuValue == 1) {
        document.getElementById('statu_Active').checked = true;
        document.getElementById('statu_Passive').checked = false;
    }
    else {
        document.getElementById('statu_Active').checked = false;
        document.getElementById('statu_Passive').checked = true;
    }
    if (e_usr.authValue == "admin") {

        document.getElementById('auth_admin').checked = true;
        document.getElementById('auth_user').checked = false;
    }
    else {
        document.getElementById('auth_admin').checked = false;
        document.getElementById('auth_user').checked = true;
    }
    document.getElementById("optLang").value = e_usr.langValue;
    document.getElementById("txtcreateTime").value = e_usr.createTime;
    document.getElementById("txtchangeTime").value = e_usr.changeTime;
    document.getElementById("txtexpireTime").value = e_usr.expireTime;

    document.getElementById("cb_Core_project").checked = e_usr.Core_project;
    document.getElementById("cb_Fason_project").checked = e_usr.Fason_project;

    document.getElementById("txtpass").disabled = true;
    document.getElementById("txtpassrepeat").disabled = true;



}
var NewUser = function () {
    localStorage.setItem(All.String.strUserIDBilgi, "0");

    document.getElementById('txtusername').value = "";
    document.getElementById('txtuseremail').value = "";
    document.getElementById('txtuseremail').disabled = false;
    document.getElementById('txtuserbackupemail').value = "";
    document.getElementById('statu_Active').checked = true;
    document.getElementById('statu_Passive').checked = false;
    document.getElementById('auth_admin').checked = false;
    document.getElementById('auth_user').checked = true;
    document.getElementById("optLang").value = 1;
    document.getElementById("txtcreateTime").value = "";
    document.getElementById("txtcreateTime").disabled = true;
    document.getElementById("txtchangeTime").value = "";
    document.getElementById("txtchangeTime").disabled = true;
    document.getElementById("txtexpireTime").value = "";
    document.getElementById("txtexpireTime").disabled = true;

    document.getElementById("cb_Core_project").checked = false;
    document.getElementById("cb_Fason_project").checked = false;

    document.getElementById("txtpass").disabled = false;
    document.getElementById("txtpassrepeat").disabled = false;
}
var Setuserpage = function () {
    if (document.getElementById('auth_admin').checked == true) {
        e_usr.authValue = "admin";
    }
    else {
        e_usr.authValue = "user";
    }
//----------
    if (document.getElementById("cb_Core_project").checked == true) {
        e_usr.Core_project = true;
    }
    else {
        e_usr.Core_project = false;
    }
//----------
    if (document.getElementById("cb_Fason_project").checked == true) {
        e_usr.Fason_project = true;
    }
    else {
        e_usr.Fason_project = false;
    }
//--------------
    if (document.getElementById('statu_Active').checked == true) {
        e_usr.statuValue = 1;
    }
    else {
        e_usr.statuValue = 0;
    }
    //-----------
    e_usr.langValue = document.getElementById("optLang").value;
    e_usr.pass = document.getElementById("txtpass").value;
    e_usr.passRepat = document.getElementById("txtpassrepeat").value;
    e_usr.userBackupMail = document.getElementById('txtuserbackupemail').value;
    e_usr.userID = localStorage.getItem(All.String.strUserIDBilgi);
    e_usr.userMail = document.getElementById('txtuseremail').value;
    e_usr.userName = document.getElementById('txtusername').value;


}
var Saveuser = function () {

    var mdl = new All.Models.cRequest();
    mdl.token = localStorage.getItem("token");
    Setuserpage();
    mdl.data = JSON.stringify(e_usr);

    var settings = {
        "url": All.webpaceoptmainuserList,
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
            //işlem sonucu olursa
        }
        else {
            //işlem sonucu olmaz ise
            $("div.warning").html(response.message);
            $("div.warning").fadeIn(All.Integer.msgFadein).delay(All.Integer.msgDelay).fadeOut(All.Integer.msgFadeout);
        }
    });
}
$(document).ready(function () {
    Getlist();

    $('#example tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            
        }
        else {
            dt.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            data_usrlist = dt.row(this).data();
            txtusername.value = data_usrlist.Kod;
            localStorage.setItem(All.String.strUserIDBilgi, data_usrlist.Id);
            Getuser();
        }
    });
    $('#btnNew').on('click', function () {
        NewUser();
    });
    $('#btnList').on('click', function () {
        Getlist()
    });
    
    GetTime01();
});
