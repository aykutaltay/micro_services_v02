import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:http/http.dart';
import 'package:intl/intl.dart';
import 'package:flutter/cupertino.dart';
import 'package:simplemrp/pages/genpages/genmain.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';
import 'package:simplemrp/models/vusrlist.dart';
import 'package:simplemrp/models/crequest.dart';
import 'package:simplemrp/models/cresponse.dart';
import 'package:simplemrp/statics/stmethods.dart';
import 'package:simplemrp/statics/strestapi.dart';
import 'package:simplemrp/statics/ststring.dart';

void main() {

  runApp(MaterialApp(
    home: UserList(),
  ));

  // getData()
  //     .then((success) => runApp(MaterialApp(
  //         home: UserList(),
  //         )));
    //   .whenComplete(() => runApp(MaterialApp(
    //   home: UserList(),
    // )));

}

List<userlist> l_usr = List<userlist>();
bool kontrl = false;


Future<List<userlist>> getData() async {

  List<userlist> l_tmp = List<userlist>();
  //verilerina lınması
  var req01 = cRequest();
  req01.prosses_code = 0;
  req01.data = "";

  cResponse aa01 =
  await stRestApi().postSec(stString().url_s + '/mainuserlist', req01).then((value) => null);

  if (aa01.message_code == 1) {
    //var qq = userlist.fromJson(jsonDecode(aa01.data));
    // var qq = jsonDecode(aa01.data)[userlist];
    // users = qq != null ? List.from(qq) : null;

    //var qq = jsonDecode(aa01.data)[userlist] as List;
    //List<userlist> qusr = qq.map((qq) => ())
    //String Tmp ='{"userlist" :'+'[{"Id":29,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Ulas ORUÇ","Email":"lal_51@hotmail.com","SonTarih":"23.09.2021 02:57:06"},{"Id":46,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Aykut ALTAY","Email":"aykutaltay@zoradamlar.com","SonTarih":"15.10.2021 19:29:16"}]'+"}";
    //String Tmp_sade ='[{"Id":29,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Ulas ORUÇ","Email":"lal_51@hotmail.com","SonTarih":"23.09.2021 02:57:06"},{"Id":46,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Aykut ALTAY","Email":"aykutaltay@zoradamlar.com","SonTarih":"15.10.2021 19:29:16"}]';

    //var qq=jsonDecode(Tmp_sade);
    //var qq = userlist.fromJson(jsonDecode(aa01.data));
    //users.add(qq[0]);

    var qq = jsonDecode(aa01.data);
    l_usr.clear();
    for (var item in qq) {
      userlist usr_tmp = userlist.fromJson(item);
      l_tmp.add(usr_tmp);
    }

    //print(l_usr);
    kontrl=true;
  }
  //vAllOfUsers.fromJson(jsonDecode(aa01.data));
  //l_usr=l_tmp;
  return l_tmp;
}

class UserList extends StatefulWidget {
  @override
  _State createState() => _State();
}

// class _State extends State<UserList> {
//   List<userlist> users = List<userlist>();
//   List<userlist> selectedUsers = List<userlist>();
//   bool sort;
//   bool load_check = false;
//
//   @override
//   void initState() {
//     getData();
//     sort = false;
//     selectedUsers = [];
//     if (load_check == true) super.initState();
//   }
//
//   Future<void> getData() async {
//     //List<userlist> l_tmp = List<userlist>();
//     //verilerina lınması
//     var req01 = cRequest();
//     req01.prosses_code = 0;
//     req01.data = "";
//
//     cResponse aa01 =
//         await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);
//
//     if (aa01.message_code == 1) {
//       //var qq = userlist.fromJson(jsonDecode(aa01.data));
//       // var qq = jsonDecode(aa01.data)[userlist];
//       // users = qq != null ? List.from(qq) : null;
//
//       //var qq = jsonDecode(aa01.data)[userlist] as List;
//       //List<userlist> qusr = qq.map((qq) => ())
//       //String Tmp ='{"userlist" :'+'[{"Id":29,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Ulas ORUÇ","Email":"lal_51@hotmail.com","SonTarih":"23.09.2021 02:57:06"},{"Id":46,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Aykut ALTAY","Email":"aykutaltay@zoradamlar.com","SonTarih":"15.10.2021 19:29:16"}]'+"}";
//       //String Tmp_sade ='[{"Id":29,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Ulas ORUÇ","Email":"lal_51@hotmail.com","SonTarih":"23.09.2021 02:57:06"},{"Id":46,"Durum":"Aktif","Yetki":"Yönetici","Kod":"Aykut ALTAY","Email":"aykutaltay@zoradamlar.com","SonTarih":"15.10.2021 19:29:16"}]';
//
//       //var qq=jsonDecode(Tmp_sade);
//       //var qq = userlist.fromJson(jsonDecode(aa01.data));
//       //users.add(qq[0]);
//
//       var qq = jsonDecode(aa01.data);
//
//       for (var item in qq) {
//         userlist usr_tmp = userlist.fromJson(item);
//         users.add(usr_tmp);
//       }
//
//       print(users);
//       load_check = true;
//     }
//     //vAllOfUsers.fromJson(jsonDecode(aa01.data));
//   }
//
//   onSortColum(int columnIndex, bool ascending) {
//     if (columnIndex == 0) {
//       if (ascending) {
//         users.sort((a, b) => a.Id.compareTo(b.Id));
//       } else {
//         users.sort((a, b) => b.Id.compareTo(a.Id));
//       }
//     }
//   }
//
//   onSelectedRow(bool selected, userlist user) async {
//     setState(() {
//       if (selected) {
//         selectedUsers.add(user);
//       } else {
//         selectedUsers.remove(user);
//       }
//     });
//   }
//
//   deleteSelected() async {
//     setState(() {
//       if (selectedUsers.isNotEmpty) {
//         List<userlist> temp = [];
//         temp.addAll(selectedUsers);
//         for (userlist user in temp) {
//           users.remove(user);
//           selectedUsers.remove(user);
//         }
//       }
//     });
//   }
//
//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         title: Text(lng_PoolStr().mainmenu.substring(0, 3) +
//             '.../' +
//             lng_PoolStr().configure_userlist),
//         leading: IconButton(
//           icon: const Icon(Icons.arrow_back),
//           tooltip: lng_PoolStr().mainmenu,
//           onPressed: () {
//             Navigator.pushReplacement(
//                 context, MaterialPageRoute(builder: (context) => MainGen()));
//           },
//         ),
//         actions: <Widget>[],
//       ),
//       body: dataBody(),
//     );
//   }
//
//   //Datatable yapan method için
//   //SingleChildScrollView
//   SingleChildScrollView dataBody() {
//     return SingleChildScrollView(
//       scrollDirection: Axis.vertical,
//       child: DataTable(
//         sortAscending: sort,
//         sortColumnIndex: 0, //ilk sütuna göre sıralayacak
//         columns: [
//           DataColumn(
//               label: Text(lng_PoolStr().id),
//               numeric: true,
//               //tooltip: "Kullanıcı ID si",
//               onSort: (columnIndex, ascending) {
//                 setState(() {
//                   sort = !sort;
//                 });
//                 onSortColum(columnIndex, ascending);
//               }),
//           DataColumn(label: Text(lng_PoolStr().auth), numeric: false),
//           DataColumn(label: Text(lng_PoolStr().code), numeric: false),
//           DataColumn(label: Text(lng_PoolStr().email), numeric: false),
//           DataColumn(label: Text(lng_PoolStr().expiredate), numeric: false)
//         ],
//         rows: users
//             .map(
//               (userlist) => DataRow(
//                   selected: selectedUsers.contains(userlist),
//                   onSelectChanged: (b) {
//                     print("OnSelect");
//                     onSelectedRow(b, userlist);
//                   },
//                   cells: [
//                     DataCell(
//                       Text(userlist.Id.toString()),
//                       onTap: () {
//                         print("Selected ${userlist.Id}");
//                       },
//                     ),
//                     DataCell(
//                       Text(userlist.Durum),
//                     ),
//                     DataCell(
//                       Text(userlist.Yetki),
//                     ),
//                     DataCell(
//                       Text(userlist.Kod),
//                     ),
//                     DataCell(
//                       Text(userlist.Email),
//                     ),
//                     DataCell(
//                       Text(userlist.SonTarih),
//                     )
//                   ]),
//             )
//             .toList(),
//       ),
//     );
//   }
//
//   @override
//   void debugFillProperties(DiagnosticPropertiesBuilder properties) {
//     super.debugFillProperties(properties);
//     properties.add(DiagnosticsProperty<bool>('load_check', load_check));
//   }
// }

// class UserList extends StatefulWidget {
//   @override
//   _StateV02 createState() => _StateV02();
// }

class _State extends State<UserList> {


  void initState() {
    getData()
      .then((value)
        {
          setState(() {
            l_usr.addAll(value);
          });
          super.initState();
        });
  //      .whenComplete(() {
  //          super.initState();
  //   });
    //super.initState();

  }


  @override



  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(lng_PoolStr().mainmenu.substring(0, 3) +
            '.../' +
            lng_PoolStr().configure_userlist),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          tooltip: lng_PoolStr().mainmenu,
          onPressed: () {
            Navigator.pushReplacement(
                context, MaterialPageRoute(builder: (context) => MainGen()));
          },
        ),
        actions: <Widget>[],
      ),
      //body: kontrl==true ?  dataBodyV02() : null,
      //body:  kontrl==true ? dataBodyV02() : getData().then((success) => dataBodyV02()),
      body: dataBodyV02(),
    );
  }

}

 SingleChildScrollView dataBodyV02() {
  int _rowsPerPage = PaginatedDataTable.defaultRowsPerPage;
  // A Variable to hold the length of table based on the condition of comparing the actual data length with the PaginatedDataTable.defaultRowsPerPage
  int _rowsPerPage1 = PaginatedDataTable.defaultRowsPerPage;
  //Obtain the data to be displayed from the Derived DataTableSource
  var dts = DTSv02();
  //dts =   getData().then((success)

  // dts.rowcount provides the actual data length, ForInstance, If we have 7 data stored in the DataTableSource Object, then we will get 12 as dts.rowCount
  var tableItemsCount = dts.rowCount;

  // PaginatedDataTable.defaultRowsPerPage provides value as 10
  var defaultRowsPerPage = PaginatedDataTable.defaultRowsPerPage;

  // We are checking whether tablesItemCount is less than the defaultRowsPerPage which means we are actually checking the length of the data in DataTableSource with default PaginatedDataTable.defaultRowsPerPage i.e, 10
  var isRowCountLessDefaultRowsPerPage = tableItemsCount < defaultRowsPerPage;

  // Assigning rowsPerPage as 10 or acutal length of our data in stored in the DataTableSource Object
  _rowsPerPage =
      isRowCountLessDefaultRowsPerPage ? tableItemsCount : defaultRowsPerPage;
   if (_rowsPerPage<=0)
       _rowsPerPage=1;

    return SingleChildScrollView(
      child: PaginatedDataTable(
        header: Text('data with 7 rows per page'),
        actions: <IconButton>[
          IconButton(
            splashColor: Colors.transparent,
            icon: const Icon(Icons.refresh),
            onPressed: () {
              dts = DTSv02();
            },
          ),
        ],
        // comparing the actual data length with the PaginatedDataTable.defaultRowsPerPage and then assigning it to _rowPerPage1 variable which then set using the setsState()
        onRowsPerPageChanged:
        isRowCountLessDefaultRowsPerPage // The source of problem!
            ? null
            : (rowCount) {
          // setState(() {
          //   _rowsPerPage1 = rowCount;
          // });
        },
        columns: <DataColumn>[
          DataColumn(label: Text('Id')),
          DataColumn(label: Text('Durum')),
          DataColumn(label: Text('Yetki')),
          DataColumn(label: Text('Kod')),
          DataColumn(label: Text('Email')),
          DataColumn(label: Text('SonTarih')),
        ],
        source: dts,
        //Set Value for rowsPerPage based on comparing the actual data length with the PaginatedDataTable.defaultRowsPerPage
        rowsPerPage:
        isRowCountLessDefaultRowsPerPage ? _rowsPerPage : _rowsPerPage1,
        showCheckboxColumn: true,
      ),
    );
}

class DemoTable extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return _DemoTableBody();
  }
}

class _DemoTableBody extends StatefulWidget {
  @override
  __DemoTableBodyState createState() => __DemoTableBodyState();
}

class __DemoTableBodyState extends State<_DemoTableBody> {
  int _rowsPerPage = PaginatedDataTable.defaultRowsPerPage;

  // A Variable to hold the length of table based on the condition of comparing the actual data length with the PaginatedDataTable.defaultRowsPerPage

  int _rowsPerPage1 = PaginatedDataTable.defaultRowsPerPage;

  @override
  Widget build(BuildContext context) {
    //Obtain the data to be displayed from the Derived DataTableSource

    var dts = DTS();

    // dts.rowcount provides the actual data length, ForInstance, If we have 7 data stored in the DataTableSource Object, then we will get 12 as dts.rowCount

    var tableItemsCount = dts.rowCount;

    // PaginatedDataTable.defaultRowsPerPage provides value as 10

    var defaultRowsPerPage = PaginatedDataTable.defaultRowsPerPage;

    // We are checking whether tablesItemCount is less than the defaultRowsPerPage which means we are actually checking the length of the data in DataTableSource with default PaginatedDataTable.defaultRowsPerPage i.e, 10

    var isRowCountLessDefaultRowsPerPage = tableItemsCount < defaultRowsPerPage;

    // Assigning rowsPerPage as 10 or acutal length of our data in stored in the DataTableSource Object

    _rowsPerPage =
        isRowCountLessDefaultRowsPerPage ? tableItemsCount : defaultRowsPerPage;
    return Scaffold(
      appBar: AppBar(
        title: Text("Demo Paginated Table"),
      ),
      body: SingleChildScrollView(
        child: PaginatedDataTable(
          header: Text('data with 7 rows per page'),
          // comparing the actual data length with the PaginatedDataTable.defaultRowsPerPage and then assigning it to _rowPerPage1 variable which then set using the setsState()
          onRowsPerPageChanged:
              isRowCountLessDefaultRowsPerPage // The source of problem!
                  ? null
                  : (rowCount) {
                      setState(() {
                        _rowsPerPage1 = rowCount;
                      });
                    },
          columns: <DataColumn>[
            DataColumn(label: Text('row')),
            DataColumn(label: Text('name')),
          ],
          source: dts,
          //Set Value for rowsPerPage based on comparing the actual data length with the PaginatedDataTable.defaultRowsPerPage
          rowsPerPage:
              isRowCountLessDefaultRowsPerPage ? _rowsPerPage : _rowsPerPage1,
        ),
      ),
    );
  }
}

class DTS extends DataTableSource {
  @override
  DataRow getRow(int index) {
    return DataRow.byIndex(
      index: index,
      cells: [
        DataCell(Text('row #$index')),
        DataCell(Text('name #$index')),
      ],
    );
  }

  @override
  int get rowCount => 9; // Manipulate this to which ever value you wish

  @override
  bool get isRowCountApproximate => false;

  @override
  int get selectedRowCount => 0;
}

class DTSv02 extends DataTableSource {

  @override

  DataRow getRow(int index) {
    final row = l_usr[index];

    return DataRow.byIndex(
      index: index,
      onSelectChanged: (value) {
        if (row.selected != value) {
          //_selectedCount += value ? 1 : -1;
          //assert(_selectedCount >= 0);
          row.selected = value;
          notifyListeners();
        }
      },
      cells: [
        DataCell(Text(l_usr[index].Id.toString())
              ,onTap: () {

            } ),
        DataCell(Text(l_usr[index].Durum)),
        DataCell(Text(l_usr[index].Yetki)),
        DataCell(Text(l_usr[index].Kod)),
        DataCell(Text(l_usr[index].Email)),
        DataCell(Text(l_usr[index].SonTarih)),
      ],
    );
  }

  @override
  int get rowCount =>
      l_usr.length; // Manipulate this to which ever value you wish

  @override
  bool get isRowCountApproximate => false;

  @override
  int get selectedRowCount => 0;
}
