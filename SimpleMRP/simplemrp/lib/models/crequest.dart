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
    cRequest result = cRequest(
        token: parsedJson['token'],
        project_code: parsedJson['project_code'],
        prosses_code: parsedJson['prosses_code'],
        data: parsedJson['data']);

    List<ex_data> l_de = new List<ex_data>();

    if (parsedJson['data_ex'] != null) {
      parsedJson['data_ex'].forEach((v) {
        l_de.add(new ex_data.fromJson(v));
      });
    }

    result.data_ex = l_de;

    return result;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> result = new Map<String, dynamic>();
    result['token'] = this.token;
    result['project_code'] = this.project_code;
    result['prosses_code'] = this.prosses_code;
    result['data'] = this.data;
    if (this.data_ex != null) {
      result['data_ex'] = this.data_ex.map((v) => v.toJson()).toList();
    } else
      result['data_ex'] = this.data_ex;

    return result;
  }
}

class ex_data {
  int id = 0;
  String info = "";
  String value = "";

  ex_data({this.id, this.info, this.value});

  factory ex_data.fromJson(Map<String, dynamic> parsedJson) {
    return ex_data(
        id: parsedJson['id'],
        info: parsedJson['info'],
        value: parsedJson['value']);
  }

  Map<String, dynamic> toJson() {
    return {'id': id, 'info': info, 'value': value};
  }
}
