import 'package:simplemrp/statics/ststring.dart';


class userlist {
  int Id = 0;
  String Durum = "";
  String Yetki = "";
  String Kod="";
  String Email="";
  String SonTarih="";

  userlist(
      {this.Id,
      this.Durum,
      this.Yetki,
      this.Kod,
      this.Email,
      this.SonTarih}
      );

  //bool selected = false;

  // factory cRequest.fromJson(Map<String, dynamic> parsedJson) {
  //   return cRequest(
  //       token: parsedJson['token'],
  //       project_code: parsedJson['project_code'],
  //       prosses_code: parsedJson['project_code'],
  //       data: parsedJson['data'],
  //       data_ex: parsedJson['data_ex']);
  // }

  factory userlist.fromJson (Map<String, dynamic> parsedJson) {
    return userlist (
      Id:parsedJson[stString().mdl_mainuserlist_header_Id] as int,
      Durum: parsedJson[stString().mdl_mainuserlist_header_Durum] as String,
      Yetki: parsedJson[stString().mdl_mainuserlist_header_Yetki] as String,
      Kod: parsedJson[stString().mdl_mainuserlist_header_Kod] as String,
      Email: parsedJson[stString().mdl_mainuserlist_header_Email] as String,
      SonTarih: parsedJson[stString().mdl_mainuserlist_header_SonTarih] as String,

    );
  }

  //factory userlist.fromJsonv02 (dynamic json) {
   // return userlist(json['Id'] as int,json['Durum'] as String,json['Yetki'] as String,json['Kod'] as String,json['EMail'] as String,json['SonTarih'] as String);
    //return userlist (json['Id'] as int, json ['Durum'] as String, json['Yetki'] as String, json['Kod'] as String,json['Email'] as String,json['SonTarih'] as String);
        // Id:parsedJson[stString().mdl_mainuserlist_header_Id],
        // Durum: parsedJson[stString().mdl_mainuserlist_header_Durum],
        // Yetki: parsedJson[stString().mdl_mainuserlist_header_Yetki],
        // Kod: parsedJson[stString().mdl_mainuserlist_header_Kod],
        // Email: parsedJson[stString().mdl_mainuserlist_header_Email],
        // SonTarih: parsedJson[stString().mdl_mainuserlist_header_SonTarih]

    //);
  //}

  Map<String, dynamic> toJson() {
    return {
      stString().mdl_mainuserlist_header_Id: Id,
      stString().mdl_mainuserlist_header_Durum: Durum,
      stString().mdl_mainuserlist_header_Yetki: Yetki,
      stString().mdl_mainuserlist_header_Kod: Kod,
      stString().mdl_mainuserlist_header_Email: Email,
      stString().mdl_mainuserlist_header_SonTarih:SonTarih
    };
  }

  @override
  String toString() {
    return '{ ${this.Id}, ${this.Durum}, ${this.Yetki}, ${this.Kod}, ${this.Email}, ${this.SonTarih} }';
  }
}