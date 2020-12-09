import 'package:flutter/material.dart';
import 'package:simplemrp/statics/ststring.dart';
import 'package:simplemrp/statics/stnumber.dart';
import 'package:simplemrp/models/vallofusers.dart';

class stPoolStr {

  static String token_0 = '';
  String get token {
    return token_0;
  }

  void set token(String value) {
    token_0 = value;
  }

  static String token_Time_0='';
  String get token_Time{
    return token_Time_0;
  }
  void set token_Time(String value){
    token_Time_0=value;
  }

  String msgError='HATA';

  static vAllOfUsers AllOfUser_0;
  void set AllOfUser (vAllOfUsers value) {
    AllOfUser_0=value;
  }
  vAllOfUsers get AllOfUser {
    return AllOfUser_0;
  }

}


