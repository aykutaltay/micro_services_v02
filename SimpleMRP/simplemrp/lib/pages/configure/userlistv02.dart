import 'dart:convert';
import 'dart:convert';
import 'dart:async';
import 'dart:io';

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
import 'package:synchronized/synchronized.dart';

List<userlist> l_usr = new List<userlist>();
final lock = new Lock();
//final DataTableSource dts;

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

  void _sort<T>(Comparable<T> getField(userlist c), int columnIndex,
      bool isSortAscending) {
    _tableDataSource?.sort(getField, isSortAscending);
    setState(() {
      _columnIndex = columnIndex;
      _isSortAscending = isSortAscending;
    });
  }

  @override
  void initState() {
    // super.initState();
    //_centers = listYukle();
  }

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
            builder: (BuildContext context, AsyncSnapshot<List<userlist>> snapshot) {
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
                      IconButton(
                        splashColor: Colors.transparent,
                        icon: const Icon(Icons.refresh),
                        onPressed: () {
                          //listYukle();
                          setState(() {});
                          //listYukle();
                        },
                      )
                    ],
                    header: Text("Başlıklar"),
                    columns: [
                      DataColumn(label: Text('Id')),
                      DataColumn(label: Text('Durum')),
                      DataColumn(label: Text('Yetki')),
                      DataColumn(label: Text('Kod')),
                      DataColumn(label: Text('Email')),
                      DataColumn(label: Text('SonTarih')),
                    ],
                    source:_tableDataSource,
                    onRowsPerPageChanged:
                        (l_usr.length < PaginatedDataTable.defaultRowsPerPage)
                            ? null
                            : (r) {
                                setState(() {
                                  _rowsPerPage = r;
                                });
                              },
                    rowsPerPage: _rowsPerPage,
                  ),
                );
              }
              if (snapshot.hasError) {
                return Center(
                    child: Container(
                        height: 250,
                        width: 250.0,
                        child: Text('${snapshot.error}')));
              }
               else {
                 return Center(
                   child: Container(
                     height: 100,
                     width: 100,
                     child: CircularProgressIndicator(),
                   ),
                 );
               }
            },
            future: listYukle(),
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
}

class DTS extends DataTableSource {
  @override
  DataRow getRow(int index) {
    if (index >= l_usr.length) return null;

    return DataRow.byIndex(
        index: index,
        onSelectChanged: (value) {
          notifyListeners();
        },
        cells: [
          DataCell(Text(l_usr[index].Id.toString())),
          DataCell(Text(l_usr[index].Durum)),
          DataCell(Text(l_usr[index].Yetki)),
          DataCell(Text(l_usr[index].Kod)),
          DataCell(Text(l_usr[index].Email)),
          DataCell(Text(l_usr[index].SonTarih)),
        ]);
  }

  @override
  bool get isRowCountApproximate => false;

  @override
  int get rowCount => l_usr.length;

  @override
  int get selectRowCount => 0;

  @override
  int get selectedRowCount => 0;
}

Future<List<userlist>> listYukle() async {
  final List<userlist> f_l_tmp = List<userlist>();
  // userlist tmpusr = userlist(
  //   Id: 1,Durum: "",Email: "",Kod: "",SonTarih: "",Yetki: ""
  // );
  // l_usr.add(tmpusr);

  bool bekleme = true;
  List<userlist> l_tmp = List<userlist>();
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
  final cResponse aa01 = await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);
    if (aa01.message_code == 1) {
      var qq = jsonDecode(aa01.data);

      for (var item in qq) {
        userlist usr_tmp = userlist.fromJson(item);
        l_tmp.add(usr_tmp);
      }

      //print(l_usr);
    };

    l_usr.clear();
    l_usr.addAll(l_tmp);
  //});

  f_l_tmp.addAll(l_usr);

  return f_l_tmp;
}

Future<void> listYukle02() async {
  await lock.synchronized(() async {
    List<userlist> l_tmp = List<userlist>();

    var req01 = cRequest();
    req01.prosses_code = 0;
    req01.data = "";
    cResponse aa01 =
        await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);

    if (aa01.message_code == 1) {
      var qq = jsonDecode(aa01.data);

      for (var item in qq) {
        userlist usr_tmp = userlist.fromJson(item);
        l_tmp.add(usr_tmp);
      }

//print(l_usr);
    }
    ;

    l_usr.clear();
    l_usr.addAll(l_tmp);
  });
}

Future<SafeArea> listYukle03() async {
  await lock.synchronized(() async {
    List<userlist> l_tmp = List<userlist>();

    var req01 = cRequest();
    req01.prosses_code = 0;
    req01.data = "";
    cResponse aa01 =
        await stRestApi().postSec(stString().url_s + '/mainuserlist', req01);

    if (aa01.message_code == 1) {
      var qq = jsonDecode(aa01.data);

      for (var item in qq) {
        userlist usr_tmp = userlist.fromJson(item);
        l_tmp.add(usr_tmp);
      }

//print(l_usr);
    }
    ;

    l_usr.clear();
    l_usr.addAll(l_tmp);
  });

  var dts = DTS();
  int _rowsPerPage = PaginatedDataTable.defaultRowsPerPage;

  return SafeArea(
    child: SingleChildScrollView(
      child: PaginatedDataTable(
        actions: [
          IconButton(
            splashColor: Colors.transparent,
            icon: const Icon(Icons.refresh),
            onPressed: () {
              listYukle();
              //setState(() {

              //});
              //listYukle();
            },
          )
        ],
        header: Text("Başlıklar"),
        columns: [
          DataColumn(label: Text('Id')),
          DataColumn(label: Text('Durum')),
          DataColumn(label: Text('Yetki')),
          DataColumn(label: Text('Kod')),
          DataColumn(label: Text('Email')),
          DataColumn(label: Text('SonTarih')),
        ],
        source: dts,
        onRowsPerPageChanged:
            (l_usr.length < PaginatedDataTable.defaultRowsPerPage)
                ? null
                : (r) {
                    // setState(() {
                    //   _rowPerPage = r;
                    // });
                  },
        rowsPerPage: _rowsPerPage,
      ),
    ),
  );
}

class CentersTableDataSource extends DataTableSource {
  final List<userlist> _centers;
  int _rowsSelectedCount = 0;

  CentersTableDataSource(this._centers);

  @override
  DataRow getRow(int index) {
    assert(index >= 0);
    if (index >= _centers.length) return null;

    final userlist center = _centers[index];
    return DataRow.byIndex(
      index: index,
      selected: center.selected,
      onSelectChanged: (selected) {
        if (center.selected != selected) {
          _rowsSelectedCount += selected ? 1 : -1;
          center.selected = selected;
          notifyListeners();
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
      Comparable<T> getField(userlist d), bool isAscending) {
    _centers.sort((a, b) {
      if (isAscending) {
        final userlist c = a;
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
    _centers.forEach((center) => center.selected = isAllChecked);
    _rowsSelectedCount = isAllChecked ? _centers.length : 0;
    notifyListeners();
  }
}
