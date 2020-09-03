using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_users
    {
        public users Saveusers(users USERS, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            users result = new users();
            BeforeSaveusers(USERS: USERS,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERS.users_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERS.users_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (USERS.users_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<users>(USERS);
                        if (id != 0)
                            result = db.Get<users>(id);
                    }
                    else
                    {
                        bool ok = db.Update<users>(USERS);
                        if (ok == true)
                            result = db.Get<users>(USERS.users_id);
                        else
                            result = USERS;
                    }
                }
            }
            AfterSaveusers(USERS: USERS, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteusers(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteusers(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                users etmp = Getusers(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.users_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.users_active = false;
                etmp.deletedusers_id = true;
                users eresulttmp = Saveusers(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedusers_id == true)
                    result = true;
            }
            AfterDeleteusers(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public users Getusers(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            users result = new users();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<users>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.users_use == false) || (result.deletedusers_id == true) || (result.users_active==false))
                            result = new users();
                }
            }
            return result;
        }
        public List<users> GetAllusers(string whereclause , string DBTYPE , string CONNSTR , bool ALL=false)
        {
            List<users> result = new List<users>();
            BeforeGetAllusers(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_users info = new info_users();
                whereclause += "AND " + info.users_deletedusers_id + " = false AND " + info.users_users_use + " = true AND " + info.users_users_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<users>(whereclause: whereclause).ToList();
                }            }            AfterGetAllusers(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveusers(users USERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveusers(users USERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteusers (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteusers(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetusers(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetusers(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllusers(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAllusers(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
