import 'dart:io';

import 'package:flutter/material.dart';
import 'package:simplemrp/statics/lang/lng_poolstr.dart';
import 'package:simplemrp/statics/lang/lng_string.dart';
import 'package:simplemrp/statics/stpoolstr.dart';

const m_stock = 'Stok';
const m_stock_stocklist = 'Stok Liste';
const m_stock_servicestocklist = 'Hizmet Stok Liste';
const m_stock_assetlist = 'Demirbaş Listesi';
const m_stock_transferbetweenwarehouse = 'Depolar Arası Transfer';
const m_stock_stockmovement = 'Stok Hareket';

const m_bpartner = 'Cari';
const m_bpartner_caseList = 'Kasa Liste';
const m_bpartner_bankList = 'Banka Liste';
const m_bpartner_bpartnerList='Cari Liste';
const m_bpartner_bpartnermovement='Cari Hareket';
const m_bparner_bill='Çek Senet';
const m_bpartner_bill_billList='Çek Senet Liste';
const m_bpartner_bill_in ='Çek Senet Giriş';
const m_bpartner_bill_out='Çek Senet Çıkış';

const m_sales='Satış';
const m_sales_salesorder ='Satış Siparişi';
const m_sales_sales='Satış';
const m_sales_retail='Perakende';

const m_buying='Alış';
const m_buying_buyorder='Alış Siparişi';
const m_buying_buying='Alış';

const m_production='Üretim';
const m_production_BOM = 'Ürün Ağacı'; //bill of metarials, bom
const m_production_productionorder='Üretim Siparişi';
const m_production_production='Üretim';

const m_reports='Raporlar';

const m_configure='Ayarlar';
const m_configure_parameterlist='Parametre Listesi';
const m_configure_userlist='Kullanıcı Listesi';

const m_quit='Çıkış';

void main() {
  runApp(MaterialApp(
    home: MainGen(),
  ));
}

class MainGen extends StatefulWidget {
  @override
  _State createState() => _State();
}

class _State extends State<MainGen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(lng_PoolStr().mainmenu),
        actions: <Widget>[],
      ),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: <Widget>[
            ListTile(
              title: Text(stPoolStr().AllOfUser.users_name),
              dense: true,
              onTap: () {},
            ),
            //stock
            ExpansionTile(
              title: Text(m_stock,
              maxLines: 1,
              style: const TextStyle(
                fontSize: 12,
                color: Colors.red
              ),
              ),
              children: const <Widget>[
                ListTile(
                  title: Text(m_stock_stocklist),
                ),
                ListTile(
                  title: Text(m_stock_servicestocklist),
                ),
                  ListTile(
                  title: Text(m_stock_assetlist),
                ),
                ListTile(
                  title: Text(m_stock_transferbetweenwarehouse),
                ),
                ListTile(
                  title: Text(m_stock_stockmovement),
                )
              ],
            ),
            //business partner
            ExpansionTile(
              title: Text(m_bpartner),
              children: const <Widget>[
                ListTile(
                  title: Text(m_bpartner_caseList),
                ),
                ListTile(
                  title: Text(m_bpartner_bankList),
                ),
                ListTile(
                  title: Text(m_bpartner_bpartnerList),
                ),
                ListTile(
                  title: Text(m_bpartner_bpartnermovement),
                ),
                ExpansionTile(
                  title: Text(m_bparner_bill),
                  children: const <Widget>[
                    ListTile  (
                      title: Text(m_bpartner_bill_billList),
                    ),
                    ListTile(
                      title: Text(m_bpartner_bill_in),
                    ),
                    ListTile(
                      title: Text(m_bpartner_bill_out),
                    )
                  ],
                )

              ],
            ),
            //Sales
            ExpansionTile(
              title: Text(m_sales),
              children: const <Widget>[
                ListTile(
                  title: Text(m_sales_salesorder),
                ),
                ListTile(
                  title: Text(m_sales_sales),
                ),
                ListTile(
                  title: Text(m_sales_retail),
                )
              ],
            ),
            //Buying
            ExpansionTile(
              title: Text(m_buying),
              children: const <Widget>[
                ListTile(
                  title: Text(m_buying_buyorder),
                ),
                ListTile(
                  title: Text(m_buying_buying),
                )
              ],
            ),
            //Üretim
            ExpansionTile(
              title: Text(m_production),
              children: const <Widget> [
                ListTile(
                  title: Text(m_production_productionorder),
                ),
                ListTile(
                  title: Text(m_production_BOM),
                ),
                ListTile(
                  title: Text (m_production_production),
                )
              ],
            ),
            //Raporlar
            ExpansionTile(
              title: Text(m_reports),
            ),
            //Ayarlar
            ExpansionTile(
              title: Text(m_configure),
              children: const <Widget>[
                ListTile(
                  title: Text(m_configure_parameterlist),
                ),
                ListTile(
                  title: Text(m_configure_userlist),
                )
              ],
            ),
            ListTile(
              title: Text(m_quit),
              onTap: () =>  exit(0),

            )
            // ListTile (
            // //  leading:  CircleAvatar(),
            //   title: Text('stPoolStr().AllOfUser.users_name'),
            // //    subtitle: Text("me@codesundar.com")
            // ),
            // ExpansionTile (
            //   //leading:  Icon(Icons.storage),
            //   title: Text('lng_PoolStr().stock'),
            //   children: <Widget>[
            // new ListTile(
            //   leading:  Icon(Icons.account_circle),
            //   title: const Text('  sdfsdfv '),
            // )
            // ListTile(
            //   title: Text(lng_PoolStr().servicestocklist),
            //   onTap: () {
            //     Navigator.pop(context);
            //   },
            // ),
            // ListTile(
            //   title: Text(lng_PoolStr().assetlist),
            //   onTap: () {
            //     Navigator.pop(context);
            //   },
            // ),
            // ListTile(
            //   title: Text(lng_PoolStr().transferbetweenwarehouse),
            //   onTap: () {
            //     Navigator.pop(context);
            //   },
            // ),
            // ListTile(
            //   title: Text(lng_PoolStr().stockmovement),
            //   onTap: () {
            //     Navigator.pop(context);
            //   },
            // )
            // ],
            // ),
            // ListTile (
            //   leading:  Icon(Icons.account_circle),
            //   title: Text('profiller'),
            // ),
            // ListTile (
            //   leading:  Icon(Icons.settings),
            //   title: Text('Ayarlar'),
            // ),
            // ExpansionTile (
            //   title: Text ('Mamulagac'),
            //   children: <Widget>[
            //     ListTile (
            //       leading: Icon(Icons.subdirectory_arrow_right),
            //       title: Text('ilk altbaşlık'),
            //     )
            //   ],
            // )
          ],
        ),
      ),
    );
  }
}
