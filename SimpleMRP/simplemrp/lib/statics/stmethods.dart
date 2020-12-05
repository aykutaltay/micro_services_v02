// ignore: camel_case_types
import 'package:simplemrp/statics/stpoolstr.dart';
import 'dart:convert';
import 'package:crypto/crypto.dart';


class stMethods {


  String generateMd5(String input) {
    return md5.convert(utf8.encode(input)).toString();
  }

  }


