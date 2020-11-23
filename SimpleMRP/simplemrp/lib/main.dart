
import 'package:flutter/material.dart';
import 'package:simplemrp/pages/genpages/genlogin.dart';
import 'package:simplemrp/statics/ststring.dart';
import 'package:simplemrp/statics/stnumber.dart';

void main() {
  runApp((LoginPage()));
}

class LoginPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) => new MaterialApp(
    debugShowCheckedModeBanner: false,
    color: Colors.redAccent,
    title: stString().projectName[stNumber().langID],
    theme: ThemeData(
      primaryColor: Color(0xb703326c),
      accentColor: Colors.indigo,
    ),
    routes: <String, WidgetBuilder>{
      '/genLogin': (BuildContext context) => LoginGen(),
    },
    home: new LoginGen(),
  );
}