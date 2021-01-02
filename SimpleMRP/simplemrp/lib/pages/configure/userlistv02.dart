import 'dart:convert';
import 'dart:convert';
import 'dart:async';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:http/http.dart';
import 'package:intl/intl.dart';
import 'package:flutter/cupertino.dart';
import 'package:simplemrp/pages/configure/userlist.dart';
import 'package:simplemrp/pages/configure/usersave.dart';
import 'package:simplemrp/pages/genpages/genmain.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';
import 'package:simplemrp/models/vusrlist.dart';
import 'package:simplemrp/models/crequest.dart';
import 'package:simplemrp/models/cresponse.dart';
import 'package:simplemrp/statics/stmethods.dart';
import 'package:simplemrp/statics/strestapi.dart';
import 'package:simplemrp/statics/ststring.dart';
import 'package:synchronized/synchronized.dart';
import 'package:simplemrp/statics/stnumber.dart';
//Sayfa içi genel değişkenler
List<UserList_loc> l_usr = new List<UserList_loc>();
final lock = new Lock();
int list_Id = 0;

//final DataTableSource dts;
//Sayfada liste ve deciç uygulanıyor ise aşağıdaki gibi "loc" uzantılı bir class yapılacaktır
//Liste buradan beslenecektir.
class UserList_loc {
  int Id = 0;
  String Durum = "";
  String Yetki = "";
  String Kod = "";
  String Email = "";
  String SonTarih = "";
  bool selected = false;

  UserList_loc({Id, Durum, Yetki, Kod, Email, SonTarih, selected});

  @override
  String toString() {
    return '{ ${Id}, ${Durum}, ${Yetki}, ${Kod}, ${Email}, ${SonTarih}, ${selected} }';
  }
}

class UserListV02 extends StatefulWidget {
  @override
  _UserListV02 createState() => _UserListV02();
}

class _UserListV02 extends State<UserListV02> {
  //int _rowPerPage = PaginatedDataTable.defaultRowsPerPage;
  //int _rowPerPage=l_usr.length;
  int _rowsPerPage = PaginatedDataTable.defaultRowsPerPage; // This one works
  int _columnIndex = 1;
  bool _isSortAscending = true;
  //Future<List<userlist>> _centers;
  CentersTableDataSource _tableDataSource;
  //var _scaffoldKey = GlobalKey<ScaffoldState>();

  void _sort<T>(Comparable<T> getField(UserList_loc c), int columnIndex,
      bool isSortAscending) {
    _tableDataSource?.sort(getField, isSortAscending);
    setState(() {
      _columnIndex = columnIndex;
      _isSortAscending = isSortAscending;
    });
  }

  @override
  void initState() {}

  @override
  Widget build(BuildContext context) {
    //listYukle02();
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
        body: Container(
          child: FutureBuilder(
            future: listYukle(),
            builder: (BuildContext context,
                AsyncSnapshot<List<UserList_loc>> snapshot) {
              if (snapshot.hasData) {
                var centerList = snapshot.data;
                var centersCount = centerList.length;
                var defaultRowsPerPage = PaginatedDataTable.defaultRowsPerPage;
                // _rowsPerPage = centersCount < defaultRowsPerPage
                //     ? centersCount
                //     : defaultRowsPerPage;
                _tableDataSource = CentersTableDataSource(centerList);
                return SingleChildScrollView(
                  child: PaginatedDataTable(
                    actions: [
                      //filter
                      IconButton(
                        splashColor: Colors.transparent,
                        tooltip: lng_PoolStr().filter,
                        color: Colors.blueAccent,
                        icon: const Icon(Icons.filter_frames),
                        onPressed: () {

                        },
                      ),
                      //refresh
                      IconButton(
                        splashColor: Colors.transparent,
                        tooltip: lng_PoolStr().refresh,
                        icon: const Icon(Icons.refresh),
                        onPressed: () {
                          //l_usr[0].Yetki='AAA';
                          setState(() {});
                        },
                      ),
                      //Add
                      IconButton(
                        splashColor: Colors.transparent,
                        tooltip: lng_PoolStr().add,
                        icon: const Icon(Icons.add),
                        onPressed: () {
                          int id=0;
                          for (var i=0; i<l_usr.length; i++) {
                            if (l_usr[i].selected==true) {
                              id = l_usr[i].Id;
                              break;
                            };
                          };
                          stNumber().DataId=id;

                          Navigator.pushReplacement(context,
                              MaterialPageRoute(builder: (context) =>UserSave()
                              ));
                        },
                      ),
                      //change
                      IconButton(
                        splashColor: Colors.transparent,
                        tooltip: lng_PoolStr().change,
                        icon: const Icon(Icons.border_color),
                        onPressed: () {},
                      ),
                      //delete
                      IconButton(
                        splashColor: Colors.transparent,
                        color: Colors.blueAccent,
                        tooltip: lng_PoolStr().delete,
                        icon: const Icon(Icons.delete),
                        onPressed: () {

                        },
                      )
                    ],
                    header: Text(""),
                    columns: [
                      DataColumn(label: Text('Id')),
                      DataColumn(label: Text('Durum')),
                      DataColumn(label: Text('Yetki')),
                      DataColumn(label: Text('Kod')),
                      DataColumn(label: Text('Email')),
                      DataColumn(label: Text('SonTarih')),
                    ],
                    source: _tableDataSource,
                    onRowsPerPageChanged:
                        (l_usr.length < PaginatedDataTable.defaultRowsPerPage)
                            ? null
                            : (r) {
                                setState(() {
                                  _rowsPerPage = r;
                                });
                              },
                    rowsPerPage: _rowsPerPage,
                    sortColumnIndex: _columnIndex,
                    sortAscending: _isSortAscending,
                    onSelectAll: (isAllChecked) =>
                        _tableDataSource?.selectAll(isAllChecked),
                  ),
                );
              }
              if (snapshot.hasError) {
                return Center(
                    child: Container(
                        height: 250,
                        width: 250.0,
                        child: Text('${snapshot.error}')));
              } else {
                return Center(
                  child: Container(
                    height: 100,
                    width: 100,
                    child: CircularProgressIndicator(),
                  ),
                );
              }
            },
          ),
        )
        // SafeArea(
        //   child: SingleChildScrollView(
        //     child: PaginatedDataTable(
        //       actions: [
        //         IconButton(
        //           splashColor: Colors.transparent,
        //           icon: const Icon(Icons.refresh),
        //           onPressed: () {
        //             listYukle();
        //             setState(() {
        //
        //
        //             });
        //             //listYukle();
        //           },
        //         )
        //       ],
        //       header: Text("Başlıklar"),
        //       columns: [
        //         DataColumn(label: Text('Id')),
        //         DataColumn(label: Text('Durum')),
        //         DataColumn(label: Text('Yetki')),
        //         DataColumn(label: Text('Kod')),
        //         DataColumn(label: Text('Email')),
        //         DataColumn(label: Text('SonTarih')),
        //       ],
        //       source: dts,
        //       onRowsPerPageChanged:
        //           (l_usr.length < PaginatedDataTable.defaultRowsPerPage)
        //               ? null
        //               : (r) {
        //                   setState(() {
        //                     _rowPerPage = r;
        //                   });
        //                 },
        //       rowsPerPage: _rowPerPage,
        //     ),
        //   ),
        // ),
        );
  }


  _showDialog() {
    showDialog(
        context: context,
        builder: (_) => new CupertinoAlertDialog(
          title: new Text(lng_PoolStr().warning),
          content: new Text("Hey! I'm Coflutter!"),
          actions: <Widget>[
            FlatButton(
              child: Text ('Evet'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            FlatButton(
              child: Text ('Kapat'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            )
          ],
        ));
  }

}

Future<List<UserList_loc>> listYukle() async {
  final List<UserList_loc> f_l_tmp = List<UserList_loc>();

  // userlist tmpusr = userlist(
  //   Id: 1,Durum: "",Email: "",Kod: "",SonTarih: "",Yetki: ""
  // );
  // l_usr.add(tmpusr);

  bool bekleme = true;
  List<userlist> l_tmp = List<userlist>();
  List<UserList_loc> l_tmp_local = List<UserList_loc>();
  //verilerina lınması
  var req01 = cRequest();
  req01.prosses_code = 0;
  req01.data = "";

  // cResponse aa01 =
  // await stRestApi().postSec(stString().url_s + '/mainuserlist', req01).then((value) => null);
  // for (var i=0; i<3; i++) {
  //
  //   if (bekleme==true) {
  //     Timer(Duration(seconds: 3), () {
  //       stRestApi().postSec(stString().url_s + '/mainuserlist', req01).then((
  //           aa01) {
  //         if (aa01.message_code == 1) {
  //           var qq = jsonDecode(aa01.data);
  //
  //           for (var item in qq) {
  //             userlist usr_tmp = userlist.fromJson(item);
  //             l_tmp.add(usr_tmp);
  //           }
  //
  //           //print(l_usr);
  //         };
  //         l_usr.clear();
  //         l_usr.addAll(l_tmp);
  //         bekleme = false;
  //       });
  // cResponse aa01 = stRestApi().postSec_sync(stString().url_s+'/mainuserlist', req01);
  //    if (aa01.message_code == 1) {
  //      var qq = jsonDecode(aa01.data);
  //
  //      for (var item in qq) {
  //        userlist usr_tmp = userlist.fromJson(item);
  //        l_tmp.add(usr_tmp);
  //      }
  //
  //      //print(l_usr);
  //    };
  //    l_usr.clear();
  //    l_usr.addAll(l_tmp);

  //cResponse aa =  await stRestApi().postSecV02(stString().url_s + '/mainuserlist', req01);
  //cResponse aa = waitFor<cResponse> stRestApi().postSec(stString().url_s + '/mainuserlist', req01);
  //stRestApi().postSec(stString().url_s + '/mainuserlist', req01).then((aa01) {
  final cResponse aa01 =
      await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);
  if (aa01.message_code == 1) {
    var qq = jsonDecode(aa01.data);

    for (var item in qq) {
      userlist usr_tmp = userlist.fromJson(item);
      l_tmp.add(usr_tmp);

      UserList_loc usr_tmp_local = UserList_loc();
      usr_tmp_local.Id = usr_tmp.Id;
      usr_tmp_local.Yetki = usr_tmp.Yetki;
      usr_tmp_local.SonTarih = usr_tmp.SonTarih;
      usr_tmp_local.Kod = usr_tmp.Kod;
      usr_tmp_local.Email = usr_tmp.Email;
      usr_tmp_local.Durum = usr_tmp.Durum;
      usr_tmp_local.selected = false;

      l_tmp_local.add(usr_tmp_local);
    }
  }
  ;

  l_usr.clear();
  l_usr.addAll(l_tmp_local);

  f_l_tmp.addAll(l_usr);

  return f_l_tmp;
}

class CentersTableDataSource extends DataTableSource {
  final List<UserList_loc> _centers;
  int _rowsSelectedCount = 0;

  CentersTableDataSource(this._centers);

  @override
  DataRow getRow(int index) {
    assert(index >= 0);
    if (index >= _centers.length) return null;

    final UserList_loc center = _centers[index];
    return DataRow.byIndex(
      index: index,
      selected: center.selected,
      onSelectChanged: (selected) {
        if (center.selected == true) {
          if (center.selected != selected) {
            _rowsSelectedCount += selected ? 1 : -1;
            center.selected = selected;
            notifyListeners();
            list_Id = 0;
          }
        } else {
          if (_rowsSelectedCount == 0) {
            if (center.selected != selected) {
              _rowsSelectedCount += selected ? 1 : -1;
              center.selected = selected;
              list_Id = center.Id;
              notifyListeners();
            }
          }
        }
      },
      cells: <DataCell>[
        DataCell(Text(_centers[index].Id.toString())),
        DataCell(Text(_centers[index].Durum)),
        DataCell(Text(_centers[index].Yetki)),
        DataCell(Text(_centers[index].Kod)),
        DataCell(Text(_centers[index].Email)),
        DataCell(Text(_centers[index].SonTarih)),
      ],
    );
  }

  @override
  bool get isRowCountApproximate => false;

  @override
  int get rowCount => _centers.length;

  @override
  int get selectedRowCount => _rowsSelectedCount;

  void sort<T extends Object>(
      Comparable<T> getField(UserList_loc d), bool isAscending) {
    _centers.sort((a, b) {
      if (isAscending) {
        final UserList_loc c = a;
        a = b;
        b = c;
      }

      final Comparable<T> aValue = getField(a);
      final Comparable<T> bValue = getField(b);
      return Comparable.compare(aValue, bValue);
    });
    notifyListeners();
  }

  void selectAll(bool isAllChecked) {
    // _centers.forEach((center) => center.selected = isAllChecked);
    // _rowsSelectedCount = isAllChecked ? _centers.length : 0;
    notifyListeners();
  }
}

// Future<void> listYukle02() async {
//   await lock.synchronized(() async {
//     List<userlist> l_tmp = List<userlist>();
//     List<UserList_loc> l_tmp_local = List<UserList_loc>();
//
//     var req01 = cRequest();
//     req01.prosses_code = 0;
//     req01.data = "";
//     cResponse aa01 =
//         await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);
//
//     if (aa01.message_code == 1) {
//       var qq = jsonDecode(aa01.data);
//
//       for (var item in qq) {
//         userlist usr_tmp = userlist.fromJson(item);
//         l_tmp.add(usr_tmp);
//       }
//     }
//     ;
//
//     for (int i = 0; i < l_tmp.length; i++) {
//       UserList_loc usr_loc = UserList_loc(
//           Id: l_tmp[i].Id,
//           Yetki: l_tmp[i].Yetki,
//           SonTarih: l_tmp[i].SonTarih,
//           Kod: l_tmp[i].Kod,
//           Email: l_tmp[i].Email,
//           Durum: l_tmp[i].Durum,
//           selected: false);
//       l_tmp_local.add(usr_loc);
//     }
//
//     l_usr.clear();
//     l_usr.addAll(l_tmp_local);
//   });
// }

// Future<SafeArea> listYukle03() async {
//   List<userlist> l_tmp = List<userlist>();
//   List<UserList_loc> l_tmp_local = List<UserList_loc>();
//
//   var req01 = cRequest();
//   req01.prosses_code = 0;
//   req01.data = "";
//   cResponse aa01 =
//       await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);
//
//   if (aa01.message_code == 1) {
//     var qq = jsonDecode(aa01.data);
//
//     for (var item in qq) {
//       userlist usr_tmp = userlist.fromJson(item);
//       l_tmp.add(usr_tmp);
//     }
//     ;
//   }
//   ;
//
//   for (int i = 0; i < l_tmp.length; i++) {
//     UserList_loc usr_loc = UserList_loc(
//         Id: l_tmp[i].Id,
//         Yetki: l_tmp[i].Yetki,
//         SonTarih: l_tmp[i].SonTarih,
//         Kod: l_tmp[i].Kod,
//         Email: l_tmp[i].Email,
//         Durum: l_tmp[i].Durum,
//         selected: false);
//     l_tmp_local.add(usr_loc);
//   }
//
//   l_usr.clear();
//   l_usr.addAll(l_tmp_local);
//
//   var dts = DTS();
//   int _rowsPerPage = PaginatedDataTable.defaultRowsPerPage;
//
//   return SafeArea(
//     child: SingleChildScrollView(
//       child: PaginatedDataTable(
//         actions: [
//           IconButton(
//             splashColor: Colors.transparent,
//             icon: const Icon(Icons.refresh),
//             onPressed: () {
//               listYukle();
//               //setState(() {
//
//               //});
//               //listYukle();
//             },
//           )
//         ],
//         header: Text("Başlıklar"),
//         columns: [
//           DataColumn(label: Text('Id')),
//           DataColumn(label: Text('Durum')),
//           DataColumn(label: Text('Yetki')),
//           DataColumn(label: Text('Kod')),
//           DataColumn(label: Text('Email')),
//           DataColumn(label: Text('SonTarih')),
//         ],
//         source: dts,
//         onRowsPerPageChanged:
//             (l_usr.length < PaginatedDataTable.defaultRowsPerPage)
//                 ? null
//                 : (r) {
//                     // setState(() {
//                     //   _rowPerPage = r;
//                     // });
//                   },
//         rowsPerPage: _rowsPerPage,
//       ),
//     ),
//   );
// }
// class DTS extends DataTableSource {
//   @override
//   DataRow getRow(int index) {
//     if (index >= l_usr.length) return null;
//
//     return DataRow.byIndex(
//         index: index,
//         onSelectChanged: (value) {
//           notifyListeners();
//         },
//         cells: [
//           DataCell(Text(l_usr[index].Id.toString())),
//           DataCell(Text(l_usr[index].Durum)),
//           DataCell(Text(l_usr[index].Yetki)),
//           DataCell(Text(l_usr[index].Kod)),
//           DataCell(Text(l_usr[index].Email)),
//           DataCell(Text(l_usr[index].SonTarih)),
//         ]);
//   }
//
//   @override
//   bool get isRowCountApproximate => false;
//
//   @override
//   int get rowCount => l_usr.length;
//
//   @override
//   int get selectRowCount => 0;
//
//   @override
//   int get selectedRowCount => 0;
// }
