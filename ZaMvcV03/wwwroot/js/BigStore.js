var All = {};
All.webpageurl = "https://localhost:44342/";
All.webpageoptauth = All.webpageurl + "OptCore/auth";
All.webpaceoptnuser = All.webpageurl + "OptCore/nuser";
All.webpaceoptmainuserList = All.webpageurl + "OptCore/mainuserlist";
All.wabpageoptmainuserget = All.webpageurl + "OptCore/mainuserget";
All.webpageoptretoken = All.webpageurl + "OptCore/tokenRenew";
All.webpageoptsaveuser = All.webpageurl + "OptCore/saveUser";
All.webpageoptsendactmail = All.webpageurl + "OptCore/usermailActivete";
All.webpageoptdeleteduser = All.webpageurl + "OptCore/deleteUserid";
All.webpageMainIndex = "Mainuser/Index";
All.webpageMain = "Mainuser/Menu";
All.webpageMainIndex = "Mainuser/Index";

All.userToken = function () {
    var usrData = localStorage.getItem('userToken');
    if (!usrData) {
        return 'empty';
    };
    return usrData;
};

All.Integer = {};
All.Integer.msg0005CorrectUsernamePass_i = 5;
All.Integer.msgFadein = 200;
All.Integer.msgFadeout = 600;
All.Integer.msgDelay = 1800;

All.String = {};
All.String.strUserIDBilgi = "xcyUOOZUIDFKSS";

All.Methods = {};
All.Methods.postJSON = function (url, data, callback) {
    return jQuery.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'type': 'POST',
        'url': url,
        'data': JSON.stringify(data),
        'dataType': 'json',
        'success': callback
    });
};
All.Methods.ValidateEmail = function (inputtext) {
    //var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    //if (inputText.value.match(mailformat)) {
    //    return true;
    //}
    //else {
    //    alert("Mail Adresinizi Kontrol Ediniz");
    //    return false;
    //}
    return true;
};


All.Models = {};
All.Models.cRequest = function () {
    this.token = '';
    this.project_code = 0;
    this.prosses_code = 0;
    this.data = '';
    this.data_ex = All.Models.exdata;
};
All.Models.exdata = function () {
    id = 0;
    info = '';
    value = '';
};
All.Models.cResponse = function () {
    this.token = '';
    this.message_code = 0;
    this.message = '';
    this.data = '';
};
All.Models.Vusercontol = function () {
    this.USERNAME = "";
    this.PASSWORD = "";
};
All.Models.VNusercontol = function () {
    this.USERNAME = "";
    this.PASSWORD = "";
    this.NAMESURNAME = "";
};
All.Models.Vmainuserlist = function () {
    this.Id = 0;
    this.Durum = "";
    this.Yetki = "";
    this.Kod = "";
    this.Email = "";
    this.SonTarih = "";
};
All.Models.Vgetuser = function () {
    this.userID = 0;
    this.statuValue = "";
    this.authValue = "";
    this.langValue = 0;
    this.userName = "";
    this.userMail = "";
    this.userBackupMail = "";
    this.pass = "";
    this.passRepat = "";
    this.Core_project = false;
    this.Fason_project = false;
    this.createTime = "";
    this.changeTime = "";
    this.expireTime = "";
};
