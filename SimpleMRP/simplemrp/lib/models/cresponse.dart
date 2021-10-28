class cResponse {
  String token="";
  int message_code=0;
  String message="";
  String data="";

  cResponse({this.token,this.message_code,this.message,this.data});

  factory cResponse.fromJson(Map<String, dynamic> parsedJson) {
    return cResponse(
        token: parsedJson['token'],
        message_code: parsedJson['message_code'],
        message: parsedJson['message'],
        data: parsedJson['data']);
  }

  Map<String, dynamic> toJson() {
    return {
      'token': token,
      'message_code': message_code,
      'message': message,
      'data': data,
    };
  }
}