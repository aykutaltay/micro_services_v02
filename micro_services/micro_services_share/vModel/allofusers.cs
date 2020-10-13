using System;
using System.Collections.Generic;
using System.Text;

namespace micro_services_share.vModel
{
    public class allofusers
    {
        //appdatabase
        public long appdatabase_id { get; set; }
        public string appdatabase_name { get; set; }
        public string appdatabase_type { get; set; }
        public string appdatabase_connstr { get; set; }
        public bool deletedappdatabase_id { get; set; }
        public bool maintenanceappdatabase { get; set; }
        public bool appdatabase_active { get; set; }
        public bool appdatabase_use { get; set; }
        //appserver
        public long appserver_id { get; set; }
        public string appserver_name { get; set; }
        public string appserver_addr { get; set; }
        public bool deletedappserver_id { get; set; }
        public bool maintenanceappserver { get; set; }
        public bool appserver_active { get; set; }
        public bool appserver_use { get; set; }
        //company
        public long company_id { get; set; }
        public string company_name { get; set; }
        public DateTime company_createtime { get; set; }
        public DateTime company_expiretime { get; set; }
        public long company_dbserver_id { get; set; }
        public long company_appserver_id { get; set; }
        public DateTime company_updatetime { get; set; }
        public bool deletedcompany_id { get; set; }
        public bool company_active { get; set; }
        public bool company_use { get; set; }
        //country
        public long country_id { get; set; }
        public string country_name { get; set; }
        public bool deletedcountry_id { get; set; }
        public bool country_use { get; set; }
        public bool country_active { get; set; }
        //dbserver
        public long dbserver_id { get; set; }
        public string dbserver_name { get; set; }
        public string dbserver_adrr { get; set; }
        public bool deleteddbserver_id { get; set; }
        public bool maintenancedbserver { get; set; }
        public bool dbserver_active { get; set; }
        public bool dbserver_use { get; set; }
        //lang
        public long lang_id { get; set; }
        public string lang_name { get; set; }
        public long lang_country_id { get; set; }
        public bool deletedlang_id { get; set; }
        public bool lang_active { get; set; }
        public bool lang_use { get; set; }
        //project
        public long projects_id { get; set; }
        public string projects_name { get; set; }
        public string projects_type { get; set; }
        public long projects_database_id { get; set; }
        public bool deletedprojects_id { get; set; }
        public bool maintenanceprojects { get; set; }
        public bool projects_active { get; set; }
        public bool projects_use { get; set; }
        //role
        public long role_id { get; set; }
        public string role_name { get; set; }
        public int role_intvalue { get; set; }
        public string role_strvalue { get; set; }
        public bool deletedrole_id { get; set; }
        public bool role_active { get; set; }
        public bool role_use { get; set; }
        //tokensofusers
        public long tokensofusers_id { get; set; }
        public long tokensofusers_users_id { get; set; }
        public string tokensofusers_token { get; set; }
        public DateTime tokensofusers_createtime { get; set; }
        public DateTime tokensofusers_expiretime { get; set; }
        public DateTime tokensofusers_refreshtime { get; set; }
        public bool deletedtokensofusers_id { get; set; }
        public bool tokensofusers_active { get; set; }
        public bool tokensofusers_use { get; set; }
        //useractivation
        public long useractivation_id { get; set; }
        public long useractivation_users_id { get; set; }
        public DateTime useractivation_createtime { get; set; }
        public string useractivation_code { get; set; }
        public bool deleteduseractivation_id { get; set; }
        public bool useractivation_active { get; set; }
        public bool useractivation_use { get; set; }
        //users
        public long users_id { get; set; }
        public string users_name { get; set; }
        public string users_mail { get; set; }
        public long users_company_id { get; set; }
        public long users_role_id { get; set; }
        public DateTime users_createtime { get; set; }
        public DateTime users_expiretime { get; set; }
        public DateTime users_updatetime { get; set; }
        public long users_lang_id { get; set; }
        public bool deletedusers_id { get; set; }
        public bool users_active { get; set; }
        public bool users_use { get; set; }
        public string users_backupmail { get; set; }
        //usersofprojects
        public long usersofprojects_id { get; set; }
        public long usersofprojects_projects_id { get; set; }
        public long usersofprojects_users_id { get; set; }
        public bool deletedusersofprojects_id { get; set; }
        public bool usersofprojects_active { get; set; }
        public bool usersofprojects_use { get; set; }

    }
}
