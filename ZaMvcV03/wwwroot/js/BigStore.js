var All = {};
All.url = '/GateOfNewWorld/NSOperation';
All.surl = '/GateOfNewWorld/SOperation';

All.userToken = function () {
    var usrData = localStorage.getItem('userToken');
    if (!usrData) {
        return 'empty';
    };
    return usrData;
};
All.postJSON = function (url, data, callback) {
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
    this.PASS = "";
}
