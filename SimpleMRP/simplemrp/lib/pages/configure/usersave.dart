import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:simplemrp/models/crequest.dart';
import 'package:simplemrp/models/cresponse.dart';
import 'package:simplemrp/pages/configure/userlistv02.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';
import 'package:simplemrp/statics/stcss.dart';
import 'package:simplemrp/statics/stnumber.dart';
import 'package:simplemrp/models/database/users.dart';
import 'package:simplemrp/statics/stpoolstr.dart';
import 'package:simplemrp/statics/strestapi.dart';
import 'package:simplemrp/statics/ststring.dart';

void main() {}

class tabs extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: UserSave(),
    );
  }
}

class UserSave extends StatefulWidget {
  @override
  _UserSave createState() => _UserSave();
}

void getUser() {
/*
  cRequest request = new cRequest()
  {
    token = model.token,
  pro = AppStaticInt.ProjectCodeCore,
  data = JsonConvert.SerializeObject(e_tmp_eusers),
  data_ex = new List<ex_data>
  {
  new ex_data() {id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SingleCrudSave },
  new ex_data() {id=1,info=AppStaticStr.SrvTable,value=new info_users().users_tablename},
  new ex_data() {id=2,info=AppStaticStr.SrvTransCrud,value=AppStaticStr.SrvSingleCrud},
  new ex_data() {id=3,info=AppStaticStr.SrvTablePrimaryKey,value=new info_users().users_users_id},
  new ex_data() {id=4,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudSave}
  }
};
*/

  if (stNumber().DataId==0)
    return;

  List<ex_data> optvalue = new List<ex_data>();
  optvalue.add(new ex_data(id: 0,info: stString().SrvSingleCrud,value: stString().SingleCrudGet));
  optvalue.add(new ex_data(id: 1,info: stString().SrvTable,value: "users"));
  optvalue.add(new ex_data(id: 2,info: stString().SrvTransCrud,value: stString().SrvSingleCrud));
  optvalue.add(new ex_data(id: 3,info: stString().SrvTablePrimaryKey,value: "users_id"));
  optvalue.add(new ex_data(id: 4,info: stString().SrvOpt,value: stString().SingleCrudGet));

  cRequest req = new cRequest(
    token: stPoolStr().token,
    project_code: stNumber().project_Code,
    data: stNumber().DataId.toString(),
    data_ex: optvalue
  );

}



class _UserSave extends State<UserSave> with TickerProviderStateMixin {
  TabController _controller;
  TextEditingController txtCont_code = TextEditingController();
  TextEditingController txtCont_email = TextEditingController();
  TextEditingController txtCont_backupemail = TextEditingController();

  bool cb_status = false;

  String dd_author = "5-Yonetici";
  String dd_lang = "1-Türkçe";
  String cbValueRoleId="";
  String cbValueLangId="";

  final List<Tab> topTabs = <Tab>[new Tab(text: 'Kullanıcı Kayıt')];

  List<String> choisces = <String>[
    lng_PoolStr().save,
    lng_PoolStr().changePass,
    lng_PoolStr().sendActivationMail
  ];

  @override
  void initState() {
    super.initState();

    _controller = TabController(vsync: this, length: 1);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void choiceAction(String choice) {
    print('Çalışıyor ' + choice + stNumber().DataId.toString());
  }

  void saveUser() async {
    users e_users = new users(
      users_id: stNumber().DataId,
      deletedusers_id: false,
      users_active: cb_status,
      users_name: txtCont_code.text,
      users_mail: txtCont_email.text,
      users_backupmail: txtCont_backupemail.text,
      users_use: true,
      users_role_id: int.parse(cbValueRoleId),
      users_lang_id: int.parse(cbValueLangId),
      users_updatetime: DateTime.now(),
    );

    var req01 = cRequest();
    req01.prosses_code = 0;
    req01.data = jsonEncode(e_users);

    cResponse aa01 = await stRestApi().postSec(stString().url_s + '/fltSaveUser', req01).then((value) => null);
    if (aa01.message_code==1)
      {
        //buraya bilgileri çağırma methodu yaz
      }

  }

  @override
  Widget build(BuildContext context) {
    //listYukle02();
    return Scaffold(
      appBar: AppBar(
        title: Text(lng_PoolStr().mainmenu.substring(0, 3) +
            '.../' +
            lng_PoolStr().configure_userlist.substring(0, 3) +
            '.../' +
            lng_PoolStr().usersave),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          tooltip: lng_PoolStr().mainmenu,
          onPressed: () {
            Navigator.pushReplacement(context,
                MaterialPageRoute(builder: (context) => UserListV02()));
          },
        ),
        actions: <Widget>[
          PopupMenuButton<String>(
            onSelected: choiceAction,
            itemBuilder: (BuildContext context) {
              return choisces.map((String choice) {
                return PopupMenuItem<String>(
                  value: choice,
                  child: Text(choice),
                );
              }).toList();
            },
          )
        ],
        bottom: TabBar(
          controller: _controller,
          tabs: topTabs,
        ),
      ),
      body: TabBarView(controller: _controller, children: [
        new Container(
            //color: Colors.black54,
            child: //Center(child: Text('Match', style: TextStyle(color: Colors.white),),),

                ListView(
          //id
          children: <Widget>[
            Padding(
              padding: EdgeInsets.fromLTRB(10, 0, 5, 0),
              child: Text(
                'ID : ' + stNumber().DataId.toString(),
                //style: stCSS().formTextStyle,//TextStyle(color: Colors.white, fontSize: 16),
              ),
            ),
            //durum
            Padding(
              padding: EdgeInsets.fromLTRB(0, 0, 5, 0),
              child: CheckboxListTile(
                title: Text(lng_PoolStr().active),
                controlAffinity: ListTileControlAffinity.trailing,
                value: cb_status,
                onChanged: (value) {
                  setState(() {
                    cb_status = value;
                  });
                },
              ),
            ),
            //role
            Padding(
              padding: EdgeInsets.fromLTRB(10, 0, 5, 0),
              child: DropdownButton<String>(
                value: dd_author,
                //style: TextStyle(color: Colors.black, fontSize: 16),
                underline: Container(
                  height: 2,
                  color: Colors.deepPurpleAccent,
                ),
                onChanged: (String newValue) {
                  setState(() {
                    dd_author = newValue;
                  });
                },
                items: <String>[
                  '5-Yonetici',
                  '8-Kullanıcı',
                  '7-Pazarlamacı'
                ].map<DropdownMenuItem<String>>((String value) {
                  List<String> tmp = value.split('-');
                  cbValueRoleId=tmp[0];
                  return DropdownMenuItem<String>(
                    value: value,
                    child: Text(
                      value,
                    ),
                  );
                }).toList(),
              ),
            ),
            //dil
            Padding(
              padding: EdgeInsets.fromLTRB(10, 0, 5, 0),
              child: DropdownButton<String>(
                value: dd_lang,
                //style: TextStyle(color: Colors.black, fontSize: 16),
                underline: Container(height: 2, color: Colors.deepPurpleAccent),
                onChanged: (String newValue) {
                  setState(() {
                    dd_lang = newValue;
                  });
                },
                items: <String>[
                  '1-Türkçe',
                ].map<DropdownMenuItem<String>>((String value) {
                  List<String> tmp = value.split('-');
                  cbValueLangId=tmp[0];
                  return DropdownMenuItem<String>(
                    value: value,
                    child: Text(value),
                  );
                }).toList(),
              ),
            ),
            //kod
            Padding(
              child: TextField(
                controller: txtCont_code,
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: "Kod",
                ),
                //style: TextStyle(color: Colors.white, fontSize: 18),
              ),
              padding: EdgeInsets.fromLTRB(10, 0, 5, 0),
            ),
            //e-mail
            Padding(
              padding: EdgeInsets.fromLTRB(10, 10, 5, 0),
              child: TextField(
                controller: txtCont_email,
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: "e-Mail",
                ),
                //style: TextStyle(color: Colors.white, fontSize: 18),
              ),
            ),
            //yedek e-mail
            Padding(
              padding: EdgeInsets.fromLTRB(10, 10, 5, 0),
              child: TextField(
                controller: txtCont_backupemail,
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: "Yedek e-Mail",
                ),
                //style: TextStyle(color: Colors.white, fontSize: 18),
              ),
            ),
            //Son Kullanma Tarihi
            Padding(
              padding: EdgeInsets.fromLTRB(10, 10, 5, 0),
              child: TextField(
                enabled: false,
                controller: txtCont_email,
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: "Son Kullanma Tarih",
                ),
                //style: TextStyle(color: Colors.white, fontSize: 18),
              ),
            ),
          ],
        )),
      ]),
    );
  }

}
