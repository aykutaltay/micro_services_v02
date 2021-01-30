class users {
  int users_id = 0;
  String users_name = "";
  String users_mail = "";
  int users_company_id = 0;
  int users_role_id = 0;
  DateTime users_createtime = DateTime.parse("2020-01-01 00:00:00");
  DateTime users_expiretime = DateTime.parse("2020-01-01 00:00:00");
  DateTime users_updatetime = DateTime.parse("2020-01-01 00:00:00");
  int users_lang_id = 0;
  bool deletedusers_id = false;
  bool users_active = false;
  bool users_use = false;
  String users_backupmail = "";

  users(
      {this.users_id,
      this.users_name,
      this.users_mail,
      this.users_company_id,
      this.users_role_id,
      this.users_createtime,
      this.users_expiretime,
      this.users_updatetime,
      this.users_lang_id,
      this.deletedusers_id,
      this.users_active,
      this.users_use,
      this.users_backupmail});

  factory users.fromJson(Map<String, dynamic> parsedJson) {
    return users(
        users_id: parsedJson['users_id'],
        users_name: parsedJson['users_name'],
        users_mail: parsedJson['users_mail'],
        users_company_id: parsedJson['users_company_id'],
        users_role_id: parsedJson['users_role_id'],
        users_createtime: parsedJson['users_createtime'],
        users_expiretime: parsedJson['users_expiretime'],
        users_updatetime: parsedJson['users_updatetime'],
        users_lang_id: parsedJson['users_lang_id'],
        deletedusers_id: parsedJson['deletedusers_id'],
        users_active: parsedJson['users_active'],
        users_use: parsedJson['users_use'],
        users_backupmail: parsedJson['users_backupmail']);
  }


  Map<String, dynamic> toJson() {
    return {
      'users_id': users_id,
      'users_name': users_name,
      'users_mail': users_mail,
      'users_company_id': users_company_id,
      'users_role_id': users_role_id,
      'users_createtime': users_createtime,
      'users_expiretime': users_expiretime,
      'users_updatetime':users_updatetime,
      'users_lang_id':users_lang_id,
      'deletedusers_id':deletedusers_id,
      'users_active':users_active,
      'users_use':users_use,
      'users_backupmail':users_backupmail
    };
  }
}
