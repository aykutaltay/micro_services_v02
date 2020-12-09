import 'dart:convert';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:simplemrp/models/crequest.dart';
import 'package:simplemrp/models/cresponse.dart';
import 'package:simplemrp/models/vallofusers.dart';
import 'package:simplemrp/statics/stnumber.dart';
import 'package:simplemrp/statics/stpoolstr.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';
import 'package:simplemrp/statics/stlanguage.dart';
import 'package:simplemrp/statics/ststring.dart';
import 'package:simplemrp/statics/strestapi.dart';
import 'package:simplemrp/statics/stmethods.dart';
import 'package:simplemrp/models/vusr.dart';
import 'package:http/http.dart' as http;
import 'package:simplemrp/pages/genpages/genmain.dart';
import 'dart:async';
import 'package:shared_preferences/shared_preferences.dart';

const String CAPTCHA_SITE_KEY = "6LcUT8kZAAAAADHqHET4v2sJQr8Ozbta4Ti9s5dj";
bool rememberMe = false;
bool autoentry = false;
//LocalStorage storage = LocalStorage('remember');
//BuildContext context;

void main() {
  runApp(MaterialApp(
    home: LoginGen(),
  ));
}

class Item {
  const Item(this.name, this.icon);
  final String name;
  final Icon icon;
}

class LoginGen extends StatefulWidget {
  @override
  _State createState() => _State();
}

void checkRemember(BuildContext contex) async {
  // if (storage.getItem(stString().rememberMe)==null)
  //   return;
  //
  // if (storage.getItem(stString().rememberMe) == true)
  //   loginManule(storage.getItem(stString().rememberU),
  //       storage.getItem(stString().rememberP), contex);

  SharedPreferences prefs_tmp = await SharedPreferences.getInstance();
  if (prefs_tmp.getBool(stString().rememberMe) == true) {
    loginManule(prefs_tmp.getString(stString().rememberU),
        prefs_tmp.getString(stString().rememberP), contex);
  }
}

void loginManule(String userName, String passW, BuildContext contex) async {
  String genPass = passW;

  //checkRemember(contex);

  SharedPreferences prefs_tmp = await SharedPreferences.getInstance();
  if (prefs_tmp.getBool(stString().rememberMe) != true)
    genPass = stMethods().generateMd5(passW);

  if (autoentry == true) return;
  autoentry = true;
  // model of user
  var q = vUsr();
  q.USERNAME = userName;
  q.PASSWORD = genPass;

  var req = cRequest();
  req.token = CAPTCHA_SITE_KEY;
  req.data = jsonEncode(q.toJson());
  req.project_code = 0;
  req.prosses_code = 0;

  String ath_url = stString().url_s + '/auth';
  //ath_url =stString().url_s+'/auth';
  String ss = jsonEncode(req.toJson());

  cResponse aa = await stRestApi().post(ath_url, req);

  if (aa.message_code == 1) {
    if (rememberMe == true) {

      SharedPreferences prefs = await SharedPreferences.getInstance();
      prefs.setBool(stString().rememberMe, rememberMe);
      prefs.setString(stString().rememberU, q.USERNAME);
      prefs.setString(stString().rememberP, q.PASSWORD);
    }

    print(aa.message);
    stPoolStr().token = aa.token;
    stPoolStr().token_Time = DateTime.now().toString();

    var req01 = cRequest();
    req01.prosses_code=0;
    req01.data="";

    cResponse aa01 = await stRestApi().postSec(stString().url_s + '/userinfo', req01);

    DateTime dt;
    dt=DateTime.parse('2020-11-28 22:16:35');
    print(dt.toString());


    if (aa01.message_code==1) {
      stPoolStr().AllOfUser=vAllOfUsers.fromJson(jsonDecode(aa01.data));

      Navigator.pushReplacement(
          contex, MaterialPageRoute(builder: (contex) => MainGen()));
    }
  }

  //Future<String> qq = stRestApi().post(ath_url, ss);

  //HttpClient client = new HttpClient();
  //client.badCertificateCallback = ((X509Certificate cert, String host, int port) => true);

  //HttpClientRequest request01 = await client.postUrl(Uri.parse(ath_url));
  //request01.headers.set('content-type', 'application/json');
  //request01.add(utf8.encode(json.encode(req)));
  //HttpClientResponse response = await request01.close();

  //String reply = await response.transform(utf8.decoder).join();

  //print(reply);

  //final http.Response response = await http.post(Uri.encodeFull(ath_url)
  //    ,headers: <String,String> {
  //      'Access-Control-Allow-Origin': '*',
  //      'Access-Control-Allow-Headers':'Origin, X-Requested-With, Content-Type, Accept',
  //      'Content-Type': 'application/json',
  //      "Authorization": "Bearer ${stPoolStr().token}",
  //    },
  //    body: ss
  //);
  //if (response.statusCode == 200) {
  //  String sonuc =  response.body.toString();
  //} else {
  //  String Sonuc01 =  stPoolStr().msgError;
  //}
}


class _State extends State<LoginGen> {
  TextEditingController nameController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  TextEditingController re_passwordController = TextEditingController();
  bool visiblePass = true;
  bool visibleTitle = true;
  bool visibleTitleforget = false;
  bool visibleLoginbutton = true;
  bool visibleForketbutton = false;
  bool visiblereturnlogin = false;
  bool visibleregisterbtn = true;
  bool visiblereturnloginforreg = false;
  bool visiblecomparepass = false;
  bool visibleSignin = false;
  bool visiblenewuser = false;
  bool visiblelasttext = true;

  int prosid = 0; //0-login, 1-şifremi unutum
  //String info_lang=stPoolStr().language;


  Icon fab = Icon(
    Icons.refresh,
  );
  bool showProgress = false;
  double progress = 0.2;

  void toggleSubmitState() {
    setState(() {
      showProgress = !showProgress;
    });
  }

  @override
  Widget build(BuildContext context) {
    checkRemember(context);

    return Scaffold(
        appBar: AppBar(
          title: Text(lng_PoolStr().projectName),
          actions: <Widget>[
          ],
        ),
        body: Padding(
            padding: EdgeInsets.all(10),
            child: ListView(
              children: <Widget>[
                Container(
                  alignment: Alignment.centerRight,
                  //padding: EdgeInsets.all(10),
                  child: Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: <Widget>[
                        DropdownButton(
                          underline: SizedBox(),
                          icon: Icon(
                            Icons.language,
                            color: Colors.blue,
                          ),
                          items: getLanguages.map((Language lang) {
                            return new DropdownMenuItem<int>(
                              value: lang.id,
                              child: new Text(lang.name),
                            );
                          }).toList(),
                          onChanged: (val) {
                            setState(() {
                              //info_lang=stString().language[val];
                              stNumber().langID = val;
                            });
                          },
                        ),
                        Text(lng_PoolStr().language),
                      ]),
                ),
                //Kullanıcı Giriş Başlığı
                Visibility(
                  visible: visibleTitle,
                  child: Container(
                      alignment: Alignment.center,
                      padding: EdgeInsets.all(10),
                      child: Text(
                        lng_PoolStr().userLogin,
                        style: TextStyle(fontSize: 20),
                      )),
                ),
                //Şifremi unuttum  Başlığı
                Visibility(
                  visible: visibleTitleforget,
                  child: Container(
                      alignment: Alignment.center,
                      padding: EdgeInsets.all(10),
                      child: Text(
                        lng_PoolStr().forgetPassword,
                        style:
                            TextStyle(fontSize: 20, color: Colors.deepPurple),
                      )),
                ),
                //Yeni kullanıcı başlığı
                Visibility(
                  visible: visiblenewuser,
                  child: Container(
                      alignment: Alignment.center,
                      padding: EdgeInsets.all(10),
                      child: Text(
                        lng_PoolStr().newUserSave,
                        style: TextStyle(fontSize: 20, color: Colors.blueGrey),
                      )),
                ),
                Container(
                  padding: EdgeInsets.all(10),
                  child: TextField(
                    controller: nameController,
                    decoration: InputDecoration(
                      border: OutlineInputBorder(),
                      labelText: lng_PoolStr().userName,
                    ),
                  ),
                ),
                //Passwort Text
                Visibility(
                  visible: visiblePass,
                  child: Container(
                    padding: EdgeInsets.fromLTRB(10, 10, 10, 0),
                    child: TextField(
                      obscureText: true,
                      controller: passwordController,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(),
                        labelText: lng_PoolStr().passWord,
                      ),
                    ),
                  ),
                ),
                //compare password
                Visibility(
                  visible: visiblecomparepass,
                  child: Container(
                    padding: EdgeInsets.all(10),
                    child: TextField(
                      obscureText: true,
                      controller: re_passwordController,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(),
                        labelText: lng_PoolStr().comparePass,
                      ),
                    ),
                  ),
                ),
                //Şifreyi unuttum olarak hazırla
                Visibility(
                  visible: visibleLoginbutton,
                  child: Row(children: <Widget>[
                    Container(
                      child: FlatButton(
                        onPressed: () {
                          setState(() {
                            visiblePass = false;
                            visibleTitle = false;
                            visibleTitleforget = true;
                            visibleLoginbutton = false;
                            visibleForketbutton = true;
                            visiblereturnlogin = true;
                            visibleregisterbtn = false;
                            visiblelasttext = false;
                            prosid = 1;
                            //forgetPassPage=stPoolStr().returnLoginPage;
                          });
                        },
                        textColor: Colors.blue,
                        child: Text(lng_PoolStr().forgetPassword),
                      ),
                    ),
                  ]),
                ),
                Container(
                  child: CheckboxListTile(
                    title: Text(lng_PoolStr().rememberme),
                    value: rememberMe,
                    onChanged: (newValue) {
                      setState(() {
                        rememberMe = newValue;
                      });
                    },
                    controlAffinity: ListTileControlAffinity
                        .leading, //  <-- leading Checkbox
                  ),
                ),
                //]),
                //),
                //Login ekranına dön
                Visibility(
                  visible: visiblereturnlogin,
                  child: FlatButton(
                    onPressed: () {
                      setState(() {
                        visiblePass = true;
                        visibleTitle = true;
                        visibleTitleforget = false;
                        visibleLoginbutton = true;
                        visibleForketbutton = false;
                        visiblereturnlogin = false;
                        visibleregisterbtn = true;
                        visiblelasttext = true;
                        prosid = 0;
                      });
                    },
                    textColor: Colors.deepPurple,
                    child: Text(lng_PoolStr().returnLoginPage),
                  ),
                ),
                //login butonu
                Visibility(
                  visible: visibleLoginbutton,
                  child: Container(
                      height: 50,
                      padding: EdgeInsets.fromLTRB(10, 0, 10, 0),
                      child: RaisedButton(
                        textColor: Colors.white,
                        color: Colors.blue,
                        child: Text(lng_PoolStr().login),
                        onPressed: () {
                          print(nameController.text);
                          print(passwordController.text);
                          loginManule(nameController.text,
                              passwordController.text, context);
                        },
                      )),
                ),
                //Şifremi unuttum butonu
                Visibility(
                  visible: visibleForketbutton,
                  child: Container(
                      height: 50,
                      padding: EdgeInsets.fromLTRB(10, 0, 10, 0),
                      child: RaisedButton(
                        textColor: Colors.white,
                        color: Colors.deepPurple,
                        child: Text(lng_PoolStr().sendnewPass),
                        onPressed: () {},
                      )),
                ),
                //Sisteme Giriş butonu
                Visibility(
                  visible: visibleSignin,
                  child: Container(
                      height: 50,
                      padding: EdgeInsets.fromLTRB(10, 0, 10, 0),
                      child: RaisedButton(
                        textColor: Colors.white,
                        color: Colors.blueGrey,
                        child: Text(lng_PoolStr().signIn),
                        onPressed: () {},
                      )),
                ),
                Container(
                    child: Row(
                  children: <Widget>[
                    Visibility(
                      visible: visiblelasttext,
                      child: Text(lng_PoolStr().notAccount),
                    ),
                    Visibility(
                      visible: visibleregisterbtn,
                      child: FlatButton(
                          textColor: Colors.blue,
                          child: Text(
                            lng_PoolStr().signIn,
                            style: TextStyle(fontSize: 20),
                          ),
                          onPressed: () {
                            //kayıt etme bilgilerini getir
                            setState(() {
                              visiblecomparepass = true;
                              visibleSignin = true;
                              visibleregisterbtn = false;
                              visiblereturnloginforreg = true;
                              visibleLoginbutton = false;
                              visiblelasttext = false;
                              visiblenewuser = true;
                              visibleTitle = false;
                            });
                          }),
                    ),
                    Visibility(
                      visible: visiblereturnloginforreg,
                      child: FlatButton(
                          textColor: Colors.blueGrey,
                          child: Text(
                            lng_PoolStr().returnLoginPage,
                            style: TextStyle(fontSize: 20),
                          ),
                          onPressed: () {
                            //Giriş ekranına geri döndür
                            setState(() {
                              visiblecomparepass = false;
                              visibleSignin = false;
                              visibleregisterbtn = true;
                              visiblereturnloginforreg = false;
                              visibleLoginbutton = true;
                              visiblelasttext = true;
                              visiblenewuser = false;
                              visibleTitle = true;
                            });
                          }),
                    ),
                  ],
                  mainAxisAlignment: MainAxisAlignment.center,
                )),
              ],
            )));
  }
}
