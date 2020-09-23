var All = {};
All.srv = "https://localhost:5001";
All.url = '/GateOfNewWorld/NSOperation';
All.surl = '/GateOfNewWorld/SOperation';

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
    this.data_ex = '';
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

