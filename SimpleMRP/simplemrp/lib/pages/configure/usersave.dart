import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:simplemrp/pages/configure/userlistv02.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';
import 'package:simplemrp/statics/stcss.dart';
import 'package:simplemrp/statics/stnumber.dart';


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


class _UserSave extends State<UserSave> with TickerProviderStateMixin {

  TabController _controller;
  TextEditingController txtCont_code = TextEditingController();
  TextEditingController txtCont_email = TextEditingController();
  TextEditingController txtCont_backupemail = TextEditingController();

  bool cb_status = false;

  String dd_author = "Yonetici";
  String dd_lang = "Türkçe";

  final List<Tab> topTabs = <Tab>[
    new Tab(text: 'Kullanıcı Kayıt'
    )
  ];

  List <String> choisces = <String> [
    'Kaydet'
    ,'Şifre Değiştir'
    ,'Activasyon Mail Gönder'
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

  void choiceAction (String choice) {
    print('Çalışıyor '+ choice+stNumber().DataId.toString());
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
        actions: <Widget> [
            PopupMenuButton <String>(
              onSelected: choiceAction ,
              itemBuilder: (BuildContext context)
              {
                return choisces.map((String choice) {
                      return PopupMenuItem<String> (
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
      body: TabBarView(controller: _controller
          , children: [
        new Container(
            //color: Colors.black54,
            child: //Center(child: Text('Match', style: TextStyle(color: Colors.white),),),

                ListView(
                    //id
                  children: <Widget>[
                  Padding(
                    padding: EdgeInsets.fromLTRB(10, 0, 5, 0),
                    child: Text(
                      'ID : '+stNumber().DataId.toString(),
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
                        height: 1,
                        //color: Colors.deepPurpleAccent,
                      ),
                      onChanged: (String newValue) {
                        setState(() {
                          dd_author = newValue;
                        });
                      },
                      items: <String>[
                        'Yonetici',
                        'Kullanıcı',
                        'Pazarlamacı',
                        'Operator'
                      ].map<DropdownMenuItem<String>>((String value) {
                        return DropdownMenuItem<String>(
                          value: value,
                          child: Text(value),
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
                      underline: Container(
                        height: 1,
                        //color: Colors.deepPurpleAccent,
                      ),
                      onChanged: (String newValue) {
                        setState(() {
                          dd_lang = newValue;
                        });
                      },
                      items: <String>[
                        'Türkçe',
                      ].map<DropdownMenuItem<String>>((String value) {
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
                    decoration: InputDecoration(border: OutlineInputBorder(),
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
                        decoration: InputDecoration(border: OutlineInputBorder(),
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
                        decoration: InputDecoration(border: OutlineInputBorder(),
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
