using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_role
    {
        public role Saverole(role ROLE, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            role result = new role();
            BeforeSaverole(ROLE: ROLE, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                ROLE.role_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                ROLE.role_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (ROLE.role_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<role>(ROLE);
                        if (id != 0)
                            result = db.Get<role>(id);
                    }
                    else
                    {
                        bool ok = db.Update<role>(ROLE);
                        if (ok == true)
                            result = db.Get<role>(ROLE.role_id);
                        else
                            result = ROLE;
                    }
                }
            }
            AfterSaverole(ROLE: ROLE, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleterole(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleterole(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                role etmp = Getrole(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.role_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.role_active = false;
                etmp.deletedrole_id = true;
                role eresulttmp = Saverole(etmp, ALLOFUSERS);
                if (eresulttmp.deletedrole_id == true)
                    result = true;
            }
            AfterDeleterole(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public role Getrole(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            role result = new role();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<role>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.role_use == false) || (result.deletedrole_id == true) || (result.role_active==false))
                            result = new role();
                }
            }
            return result;
        }
        public List<role> GetAllrole(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<role> result = new List<role>();
            BeforeGetAllrole(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_role info = new info_role();
                whereclause += "AND " + info.role_deletedrole_id + " = false AND " + info.role_role_use + " = true AND " + info.role_role_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<role>(whereclause: whereclause).ToList();
                }            }            AfterGetAllrole(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaverole(role ROLE, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaverole(role ROLE, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleterole (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleterole(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetrole(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetrole(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllrole(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllrole(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
