import 'dart:core';
import 'package:intl/intl.dart';

class vAllOfUsers {
  //appdatabase
  int appdatabase_id;
  String appdatabase_name;
  String appdatabase_type;
  String appdatabase_connstr;
  bool deletedappdatabase_id;
  bool maintenanceappdatabase;
  bool appdatabase_active;
  bool appdatabase_use;
//appserver
  int appserver_id;
  String appserver_name;
  String appserver_addr;
  bool deletedappserver_id;
  bool maintenanceappserver;
  bool appserver_active;
  bool appserver_use;
//company
  int company_id;
  String company_name;
  DateTime company_createtime;
  DateTime company_expiretime;
  int company_dbserver_id;
  int company_appserver_id;
  DateTime company_updatetime;
  bool deletedcompany_id;
  bool company_active;
  bool company_use;
//country
  int country_id;
  String country_name;
  bool deletedcountry_id;
  bool country_use;
  bool country_active;
//dbserver
  int dbserver_id;
  String dbserver_name;
  String dbserver_adrr;
  bool deleteddbserver_id;
  bool maintenancedbserver;
  bool dbserver_active;
  bool dbserver_use;
//lang
  int lang_id;
  String lang_name;
  int lang_country_id;
  bool deletedlang_id;
  bool lang_active;
  bool lang_use;
//project
  int projects_id;
  String projects_name;
  String projects_type;
  int projects_database_id;
  bool deletedprojects_id;
  bool maintenanceprojects;
  bool projects_active;
  bool projects_use;
//role
  int role_id;
  String role_name;
  int role_intvalue;
  String role_strvalue;
  bool deletedrole_id;
  bool role_active;
  bool role_use;
//tokensofusers
  int tokensofusers_id;
  int tokensofusers_users_id;
  String tokensofusers_token;
  DateTime tokensofusers_createtime;
  DateTime tokensofusers_expiretime;
  DateTime tokensofusers_refreshtime;
  bool deletedtokensofusers_id;
  bool tokensofusers_active;
  bool tokensofusers_use;
//useractivation
  int useractivation_id;
  int useractivation_users_id;
  DateTime useractivation_createtime;
  String useractivation_code;
  bool deleteduseractivation_id;
  bool useractivation_active;
  bool useractivation_use;
//users
  int users_id;
  String users_name;
  String users_mail;
  int users_company_id;
  int users_role_id;
  DateTime users_createtime;
  DateTime users_expiretime;
  DateTime users_updatetime;
  int users_lang_id;
  bool deletedusers_id;
  bool users_active;
  bool users_use;
  String users_backupmail;
//usersofprojects
  int usersofprojects_id;
  int usersofprojects_projects_id;
  int usersofprojects_users_id;
  bool deletedusersofprojects_id;
  bool usersofprojects_active;
  bool usersofprojects_use;

  vAllOfUsers(
      {this.appdatabase_id,
      this.appdatabase_active,
      this.appdatabase_name,
      this.appdatabase_type,
      this.appdatabase_connstr,
      this.deletedappdatabase_id,
      this.appdatabase_use,
      this.maintenanceappdatabase,
      this.appserver_id,
      this.appserver_active,
      this.appserver_name,
      this.appserver_addr,
      this.company_appserver_id,
      this.deletedappserver_id,
      this.appserver_use,
      this.maintenanceappserver,
      this.company_id,
      this.company_active,
      this.company_name,
      this.company_dbserver_id,
      this.company_updatetime,
      this.company_createtime,
      this.company_expiretime,
      this.deletedcompany_id,
      this.company_use,
      this.country_id,
      this.country_active,
      this.country_name,
      this.deletedcountry_id,
      this.country_use,
      this.dbserver_id,
      this.dbserver_active,
      this.dbserver_name,
      this.dbserver_adrr,
      this.deleteddbserver_id,
      this.dbserver_use,
      this.maintenancedbserver,
      this.lang_id,
      this.lang_active,
      this.lang_name,
      this.deletedlang_id,
      this.lang_use,
      this.lang_country_id,
      this.projects_id,
      this.projects_active,
      this.projects_name,
      this.projects_type,
      this.projects_database_id,
      this.projects_use,
      this.deletedprojects_id,
      this.maintenanceprojects,
      this.role_id,
      this.role_active,
      this.role_intvalue,
      this.role_strvalue,
      this.role_name,
      this.deletedrole_id,
      this.role_use,
      this.tokensofusers_id,
      this.tokensofusers_active,
      this.tokensofusers_users_id,
      this.tokensofusers_token,
      this.tokensofusers_refreshtime,
      this.tokensofusers_createtime,
      this.tokensofusers_expiretime,
      this.deletedtokensofusers_id,
      this.tokensofusers_use,
      this.useractivation_id,
      this.useractivation_active,
      this.useractivation_code,
      this.useractivation_createtime,
      this.useractivation_users_id,
      this.deleteduseractivation_id,
      this.useractivation_use,
      this.users_id,
      this.users_active,
      this.users_backupmail,
      this.users_mail,
      this.users_name,
      this.users_createtime,
      this.users_expiretime,
      this.users_updatetime,
      this.users_lang_id,
      this.users_role_id,
      this.users_company_id,
      this.deletedusers_id,
      this.users_use,
      this.usersofprojects_id,
      this.usersofprojects_active,
      this.usersofprojects_projects_id,
      this.usersofprojects_users_id,
      this.deletedusersofprojects_id,
      this.usersofprojects_use});


factory vAllOfUsers.fromJson(Map<String, dynamic> parsedJson) {
return vAllOfUsers(
    appdatabase_id: parsedJson['appdatabase_id']
    , appdatabase_active: parsedJson['appdatabase_active']
    , appdatabase_name: parsedJson['appdatabase_name']
    , appdatabase_type: parsedJson['appdatabase_type']
    , appdatabase_connstr: parsedJson['appdatabase_connstr']
    , deletedappdatabase_id: parsedJson['deletedappdatabase_id']
    , appdatabase_use: parsedJson['appdatabase_use']
    , maintenanceappdatabase: parsedJson['maintenanceappdatabase']
    , appserver_id: parsedJson['appserver_id']
    , appserver_active: parsedJson['appserver_active']
    , appserver_name: parsedJson['appserver_name']
    , appserver_addr: parsedJson['appserver_addr']
    , company_appserver_id: parsedJson['company_appserver_id']
    , deletedappserver_id: parsedJson['deletedappserver_id']
    , appserver_use: parsedJson['appserver_use']
    , maintenanceappserver: parsedJson['maintenanceappserver']
    , company_id: parsedJson['company_id']
    , company_active: parsedJson['company_active']
    , company_name: parsedJson['company_name']
    , company_dbserver_id: parsedJson['company_dbserver_id']
    , company_updatetime: DateTime.parse(parsedJson['company_updatetime'])
    , company_createtime: DateTime.parse(parsedJson['company_createtime'])
    , company_expiretime: DateTime.parse(parsedJson['company_expiretime'])
    , deletedcompany_id: parsedJson['deletedcompany_id']
    , company_use: parsedJson['company_use']
    , country_id: parsedJson['country_id']
    , country_active: parsedJson['country_active']
    , country_name: parsedJson['country_name']
    , deletedcountry_id: parsedJson['deletedcountry_id']
    , country_use: parsedJson['country_use']
    , dbserver_id: parsedJson['dbserver_id']
    , dbserver_active: parsedJson['dbserver_active']
    , dbserver_name: parsedJson['dbserver_name']
    , dbserver_adrr: parsedJson['dbserver_adrr']
    , deleteddbserver_id: parsedJson['deleteddbserver_id']
    , dbserver_use: parsedJson['dbserver_use']
    , maintenancedbserver: parsedJson['maintenancedbserver']
    , lang_id: parsedJson['lang_id']
    , lang_active: parsedJson['lang_active']
    , lang_name: parsedJson['lang_name']
    , deletedlang_id: parsedJson['deletedlang_id']
    , lang_use: parsedJson['lang_use']
    , lang_country_id: parsedJson['lang_country_id']
    , projects_id: parsedJson['projects_id']
    , projects_active: parsedJson['projects_active']
    , projects_name: parsedJson['projects_name']
    , projects_type: parsedJson['projects_type']
    , projects_database_id: parsedJson['projects_database_id']
    , projects_use: parsedJson['projects_use']
    , deletedprojects_id: parsedJson['deletedprojects_id']
    , maintenanceprojects: parsedJson['maintenanceprojects']
    , role_id: parsedJson['role_id']
    , role_active: parsedJson['role_active']
    , role_intvalue: parsedJson['role_intvalue']
    , role_strvalue: parsedJson['role_strvalue']
    , role_name: parsedJson['role_name']
    , deletedrole_id: parsedJson['deletedrole_id']
    , role_use: parsedJson['role_use']
    , tokensofusers_id: parsedJson['tokensofusers_id']
    , tokensofusers_active: parsedJson['tokensofusers_active']
    , tokensofusers_users_id: parsedJson['tokensofusers_users_id']
    , tokensofusers_token: parsedJson['tokensofusers_token']
    , tokensofusers_refreshtime: DateTime.parse(parsedJson['tokensofusers_refreshtime'])
    , tokensofusers_createtime: DateTime.parse(parsedJson['tokensofusers_createtime'])
    , tokensofusers_expiretime: DateTime.parse(parsedJson['tokensofusers_expiretime'])
    , deletedtokensofusers_id: parsedJson['deletedtokensofusers_id']
    , tokensofusers_use: parsedJson['tokensofusers_use']
    , useractivation_id: parsedJson['useractivation_id']
    , useractivation_active: parsedJson['useractivation_active']
    , useractivation_code: parsedJson['useractivation_code']
    , useractivation_createtime: DateTime.parse(parsedJson['useractivation_createtime'])
    , useractivation_users_id: parsedJson['useractivation_users_id']
    , deleteduseractivation_id: parsedJson['deleteduseractivation_id']
    , useractivation_use: parsedJson['useractivation_use']
    , users_id: parsedJson['users_id']
    , users_active: parsedJson['users_active']
    , users_backupmail: parsedJson['users_backupmail']
    , users_mail: parsedJson['users_mail']
    , users_name: parsedJson['users_name']
    , users_createtime: DateTime.parse(parsedJson['users_createtime'])
    , users_expiretime: DateTime.parse(parsedJson['users_expiretime'])
    , users_updatetime: DateTime.parse(parsedJson['users_updatetime'])
    , users_lang_id: parsedJson['users_lang_id']
    , users_role_id: parsedJson['users_role_id']
    , users_company_id: parsedJson['users_company_id']
    , deletedusers_id: parsedJson['deletedusers_id']
    , users_use: parsedJson['users_use']
    , usersofprojects_id: parsedJson['usersofprojects_id']
    , usersofprojects_active: parsedJson['usersofprojects_active']
    , usersofprojects_projects_id: parsedJson['usersofprojects_projects_id']
    , usersofprojects_users_id: parsedJson['usersofprojects_users_id']
    , deletedusersofprojects_id: parsedJson['deletedusersofprojects_id']
    , usersofprojects_use: parsedJson['usersofprojects_use']
);
}

  Map<String, dynamic> toJson() {
    return {
      'appdatabase_id':appdatabase_id,
      'appdatabase_active':appdatabase_active,
      'appdatabase_name':appdatabase_name,
      'appdatabase_type':appdatabase_type,
      'appdatabase_connstr':appdatabase_connstr,
      'deletedappdatabase_id':deletedappdatabase_id,
      'appdatabase_use':appdatabase_use,
      'maintenanceappdatabase':maintenanceappdatabase,
      'appserver_id':appserver_id,
      'appserver_active':appserver_active,
      'appserver_name':appserver_name,
      'appserver_addr':appserver_addr,
      'company_appserver_id':company_appserver_id,
      'deletedappserver_id':deletedappserver_id,
      'appserver_use':appserver_use,
      'maintenanceappserver':maintenanceappserver,
      'company_id':company_id,
      'company_active':company_active,
      'company_name':company_name,
      'company_dbserver_id':company_dbserver_id,
      'company_updatetime':company_updatetime,
      'company_createtime':company_createtime,
      'company_expiretime':company_expiretime,
      'deletedcompany_id':deletedcompany_id,
      'company_use':company_use,
      'country_id':country_id,
      'country_active':country_active,
      'country_name':country_name,
      'deletedcountry_id':deletedcountry_id,
      'country_use':country_use,
      'dbserver_id':dbserver_id,
      'dbserver_active':dbserver_active,
      'dbserver_name':dbserver_name,
      'dbserver_adrr':dbserver_adrr,
      'deleteddbserver_id':deleteddbserver_id,
      'dbserver_use':dbserver_use,
      'maintenancedbserver':maintenancedbserver,
      'lang_id':lang_id,
      'lang_active':lang_active,
      'lang_name':lang_name,
      'deletedlang_id':deletedlang_id,
      'lang_use':lang_use,
      'lang_country_id':lang_country_id,
      'projects_id':projects_id,
      'projects_active':projects_active,
      'projects_name':projects_name,
      'projects_type':projects_type,
      'projects_database_id':projects_database_id,
      'projects_use':projects_use,
      'deletedprojects_id':deletedprojects_id,
      'maintenanceprojects':maintenanceprojects,
      'role_id':role_id,
      'role_active':role_active,
      'role_intvalue':role_intvalue,
      'role_strvalue':role_strvalue,
      'role_name':role_name,
      'deletedrole_id':deletedrole_id,
      'role_use':role_use,
      'tokensofusers_id':tokensofusers_id,
      'tokensofusers_active':tokensofusers_active,
      'tokensofusers_users_id':tokensofusers_users_id,
      'tokensofusers_token':tokensofusers_token,
      'tokensofusers_refreshtime':tokensofusers_refreshtime,
      'tokensofusers_createtime':tokensofusers_createtime,
      'tokensofusers_expiretime':tokensofusers_expiretime,
      'deletedtokensofusers_id':deletedtokensofusers_id,
      'tokensofusers_use':tokensofusers_use,
      'useractivation_id':useractivation_id,
      'useractivation_active':useractivation_active,
      'useractivation_code':useractivation_code,
      'useractivation_createtime':useractivation_createtime,
      'useractivation_users_id':useractivation_users_id,
      'deleteduseractivation_id':deleteduseractivation_id,
      'useractivation_use':useractivation_use,
      'users_id':users_id,
      'users_active':users_active,
      'users_backupmail':users_backupmail,
      'users_mail':users_mail,
      'users_name':users_name,
      'users_createtime':users_createtime,
      'users_expiretime':users_expiretime,
      'users_updatetime':users_updatetime,
      'users_lang_id':users_lang_id,
      'users_role_id':users_role_id,
      'users_company_id':users_company_id,
      'deletedusers_id':deletedusers_id,
      'users_use':users_use,
      'usersofprojects_id':usersofprojects_id,
      'usersofprojects_active':usersofprojects_active,
      'usersofprojects_projects_id':usersofprojects_projects_id,
      'usersofprojects_users_id':usersofprojects_users_id,
      'deletedusersofprojects_id':deletedusersofprojects_id,
      'usersofprojects_use':usersofprojects_use,
      'usersofprojects_users_id':usersofprojects_users_id

    };
  }
}