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
    return lng_PoolStr().stocklist[langID];
}
String get servicestocklist {
    return lng_PoolStr().servicestocklist[langID];
}
String get assetlist {
    return lng_PoolStr().assetlist[langID];
}
String get transferbetweenwarehouse{
    return lng_PoolStr().transferbetweenwarehouse[langID];
}
String get stockmovement{
    return lng_PoolStr().stockmovement[langID];
}
}