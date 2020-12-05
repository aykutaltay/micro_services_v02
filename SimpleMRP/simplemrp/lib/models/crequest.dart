class cRequest {
  String token = "";
  int project_code = 0;
  int prosses_code = 0;
  String data = "";
  List<ex_data> data_ex;

  cRequest(
      {this.token,
      this.project_code,
      this.prosses_code,
      this.data,
      this.data_ex});

  factory cRequest.fromJson(Map<String, dynamic> parsedJson) {
    return cRequest(
        token: parsedJson['token'],
        project_code: parsedJson['project_code'],
        prosses_code: parsedJson['project_code'],
        data: parsedJson['data'],
        data_ex: parsedJson['data_ex']);
  }

  Map<String, dynamic> toJson() {
    return {
      'token': token,
      'project_code': project_code,
      'prosses_code': prosses_code,
      'data': data,
      'data_ex': data_ex
    };
  }
}

class ex_data {
  int id = 0;
  String info = "";
  String value = "";

  ex_data({this.id, this.info, this.value});
}
