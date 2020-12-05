class vUsr {
  String USERNAME;
  String PASSWORD;

  vUsr({this.USERNAME, this.PASSWORD});

  factory vUsr.fromJson(Map<String, dynamic> parsedJson) {
    return vUsr(
        USERNAME: parsedJson['USERNAME'], PASSWORD: parsedJson['PASSWORD']);
  }

  Map<String, dynamic> toJson() {
    return {'USERNAME': USERNAME, 'PASSWORD': PASSWORD};
  }
}
