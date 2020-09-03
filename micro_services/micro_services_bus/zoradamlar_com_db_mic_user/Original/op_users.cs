using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_users
    {
        public users Saveusers(users USERS, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            users result = new users();
            BeforeSaveusers(USERS: USERS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERS.users_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERS.users_active = false;
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
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
            AfterSaveusers(USERS: USERS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteusers(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteusers(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                users etmp = Getusers(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.users_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.users_active = false;
                etmp.deletedusers_id = true;
                users eresulttmp = Saveusers(etmp, ALLOFUSERS);
                if (eresulttmp.deletedusers_id == true)
                    result = true;
            }
            AfterDeleteusers(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public users Getusers(long ID, allofusers ALLOFUSERS, bool ALL = false)
        {
            users result = new users();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.Get<users>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL == false)
                        if ((result.users_use == false) || (result.deletedusers_id == true) || (result.users_active == false))
                            result = new users();
                }
            }
            return result;
        }
        public List<users> GetAllusers(string whereclause, allofusers ALLOFUSERS, bool ALL = false)
        {
            List<users> result = new List<users>();
            BeforeGetAllusers(whereclause, ALLOFUSERS, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_users info = new info_users();
                whereclause += "AND " + info.users_deletedusers_id + " = false AND " + info.users_users_use + " = true AND " + info.users_users_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<users>(whereclause: whereclause).ToList();
                }
            }
            AfterGetAllusers(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveusers(users USERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaveusers(users USERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleteusers(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleteusers(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllusers(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetAllusers(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
