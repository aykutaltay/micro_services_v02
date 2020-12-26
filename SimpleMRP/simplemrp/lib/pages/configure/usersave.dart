import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:simplemrp/pages/configure/userlistv02.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';

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

  bool cb_status = false;

  String dd_author = "Yonetici";

  final List<Tab> topTabs = <Tab>[
    new Tab(text: 'Profile'),
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
        actions: <Widget>[],
        bottom: TabBar(
          controller: _controller,
          tabs: topTabs,
        ),
      ),
      body: TabBarView(controller: _controller, children: [
        new Container(
            color: Colors.black54,
            child: //Center(child: Text('Match', style: TextStyle(color: Colors.white),),),
                ListView(
              children: <Widget>[
                Padding(
                  padding: const EdgeInsets.all(3.0),
                  child: Text(
                    "ID : 5",
                    style: TextStyle(color: Colors.white, fontSize: 16),
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.all(1.0),
                  child: CheckboxListTile(
                    title: Text("Durum"),
                    controlAffinity: ListTileControlAffinity.trailing,
                    value: cb_status,
                    onChanged: (value) {
                      setState(() {
                        cb_status = value;
                      });
                    },
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.all(1.0),
                  child: DropdownButton<String>(
                    value: dd_author,
                    style: TextStyle(color: Colors.black, fontSize: 16),
                    underline: Container(
                      height: 2,
                      color: Colors.deepPurpleAccent,
                    ),
                    onChanged: (String newValue) {
                      setState(() {
                        dd_author = newValue;
                      });
                    },
                    items: <String>['Yonetici', 'Kullanıcı', 'Pazarlamacı', 'Operator']
                        .map<DropdownMenuItem<String>>((String value) {
                      return DropdownMenuItem<String>(
                        value: value,
                        child: Text(value),
                      );
                    }).toList(),
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.all(1.0),
                  child: TextField(
                    decoration: InputDecoration(
                      border: OutlineInputBorder(),
                      labelText: "Kod",
                    ),
                    style: TextStyle(color: Colors.white, fontSize: 18),
                  ),
                ),
              ],
            )),
      ]),
    );
  }
}
