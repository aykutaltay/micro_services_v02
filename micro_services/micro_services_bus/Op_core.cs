using micro_services_dal;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using micro_services_share;
using micro_services_share.vModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace micro_services_bus
{
    public class Op_core
    {
        public List<allofusers> l_allofusers (long userID,long projectID,string DBTYPE, string CONNSTR)
        {
            List<allofusers> result = new List<allofusers>();

            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    string sql = string.Format(@"
                        select * 
                        from users as usr
                        inner join usersofprojects as usr_pro on usr.users_id=usr_pro.usersofprojects_users_id and usr_pro.deletedusersofprojects_id=false
                        left join useractivation as usr_act on usr.users_id=usr_act.useractivation_users_id and usr_act.deleteduseractivation_id=false
                        left join tokensofusers as tok_usr on usr.users_id=tok_usr.tokensofusers_users_id and tok_usr.deletedtokensofusers_id=false
                        left join role as ro on usr.users_role_id=ro.role_id and ro.deletedrole_id=false 
                        inner join projects as pro on pro.projects_id=usr_pro.usersofprojects_projects_id and pro.deletedprojects_id=false
                        left join lang as la on la.lang_id=usr.users_lang_id and la.deletedlang_id=false
                        left join country as cou on cou.country_id=la.lang_country_id and cou.deletedcountry_id=false
                        inner join company as comp on comp.company_id=usr.users_company_id and comp.deletedcompany_id=false
                        left join dbserver as dbsrv on dbsrv.dbserver_id=comp.company_dbserver_id and dbsrv.deleteddbserver_id=false
                        left join appserver as apps on apps.appserver_id=comp.company_appserver_id and apps.deletedappserver_id=false
                        left join appdatabase as appdb on appdb.appdatabase_id =pro.projects_appdatabase_id and appdb.deletedappdatabase_id=false
                        where 1=1
                        And usr.users_id={0}
                        And pro.projects_id={1}
                    ",userID
                    , projectID);

                    result = db.Query<allofusers>(sql).ToList();
                }
            }

            return result;
        }
    }
}
