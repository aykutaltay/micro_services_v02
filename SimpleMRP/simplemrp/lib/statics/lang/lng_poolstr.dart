import 'package:simplemrp/statics/lang/lng_string.dart';
import 'package:simplemrp/statics/stnumber.dart';
class lng_PoolStr {
  int get langID {
    if (stNumber().langID > 1)
      return 0;
    else
      return stNumber().langID;
  }

  String get userLoginPage {
    return lng_String().userLoginPage[langID];
  }

  String get projectName {
    return lng_String().projectName[langID];
  }

  String get userLogin {
    return lng_String().userLogin[langID];
  }

  String get userName {
    return lng_String().userName[langID];
  }

  String get passWord {
    return lng_String().passWord[langID];
  }

  String get forgetPassword {
    return lng_String().forgetPassword[langID];
  }

  String get returnLoginPage {
    return lng_String().returnLoginPage[langID];
  }

  String get login {
    return lng_String().login[langID];
  }

  String get notAccount {
    return lng_String().notAccount[langID];
  }

  String get signIn {
    return lng_String().signIn[langID];
  }

  String get sendnewPass {
    return lng_String().sendnewPass[langID];
  }

  String get comparePass {
    return lng_String().comparePass[langID];
  }

  String get newUserSave {
    return lng_String().newUserSave[langID];
  }

  String get cancel {
    return lng_String().cancel[langID];
  }

  String get language {
    return lng_String().language[langID];
  }

  String get rememberme {
    return lng_String().rememberme[langID];
  }

 String get mainmenu {
    return lng_String().mainmenu[langID];
  }

  String get stock {
    return lng_String().stock[langID];
  }
String get stocklist {
    return lng_String().stocklist[langID];
}
String get servicestocklist {
    return lng_String().servicestocklist[langID];
}
String get assetlist {
    return lng_String().assetlist[langID];
}
String get transferbetweenwarehouse{
    return lng_String().transferbetweenwarehouse[langID];
}
String get stockmovement{
    return lng_String().stockmovement[langID];
}
String get bpartner{
    return lng_String().bpartner[langID];
}
String get bpartnerlist {
    return lng_String().bpartnerlist[langID];
}
String get bpartner_caselist{
    return lng_String().bpartner_caselist[langID];
}
String get bpartner_banklist{
    return lng_String().bpartner_banklist[langID];
}
String get bpartner_bpartnermovement{
    return lng_String().bpartner_bpartnermovement[langID];
}
String get bpartner_bill_billList{
    return lng_String().bpartner_bill_billList[langID];
}
String get bpartner_bill_movement{
    return lng_String().bpartner_bill_movement[langID];
}
String get sales_salesorder{
    return lng_String().sales_salesorder[langID];
}
String get sales{
    return lng_String().sales[langID];
}
String get sales_retail{
    return lng_String().sales_retail[langID];
}
String get configure{
    return lng_String().configure[langID];
}
String get configure_parameterlist{
    return lng_String().configure_parameterlist[langID];
}
String get configure_userlist{
    return lng_String().configure_userlist[langID];
}
String get buying{
    return lng_String().buying[langID];
}
String get buying_buyorder{
    return lng_String().buying_buyorder[langID];
}
String get production_productionorder{
    return lng_String().production_productionorder[langID];
}
String get production_BOM{
    return lng_String().production_BOM[langID];
}
String get production{
    return lng_String().production[langID];
}
String get reports{
    return lng_String().reports[langID];
}
String get quit {
    return lng_String().quit[langID];
}
String get id{
    return lng_String().id[langID];
}
String get status{
    return lng_String().status[langID];
}
String get auth{
    return lng_String().auth[langID];
}
String get code{
    return lng_String().code[langID];
}
String get email{
    return lng_String().email[langID];
}
String get expiredate{
    return lng_String().expiredate[langID];
}
String get warning{
    return lng_String().warning[langID];
}
String get filter {
    return lng_String().filter[langID];
}
String get refresh {
    return lng_String().refresh[langID];
}
String get add {
    return lng_String().add[langID];
}
String get change {
    return lng_String().change[langID];
}
String get delete {
    return lng_String().delete[langID];
}
String get usersave {
    return lng_String().usersave[langID];
}
}