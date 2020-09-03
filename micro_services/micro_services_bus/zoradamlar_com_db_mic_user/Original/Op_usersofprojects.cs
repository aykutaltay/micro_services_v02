using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_usersofprojects
    {
        public usersofprojects Saveusersofprojects(usersofprojects USERSOFPROJECTS, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            usersofprojects result = new usersofprojects();
            BeforeSaveusersofprojects(USERSOFPROJECTS: USERSOFPROJECTS,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERSOFPROJECTS.usersofprojects_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERSOFPROJECTS.usersofprojects_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (USERSOFPROJECTS.usersofprojects_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<usersofprojects>(USERSOFPROJECTS);
                        if (id != 0)
                            result = db.Get<usersofprojects>(id);
                    }
                    else
                    {
                        bool ok = db.Update<usersofprojects>(USERSOFPROJECTS);
                        if (ok == true)
                            result = db.Get<usersofprojects>(USERSOFPROJECTS.usersofprojects_id);
                        else
                            result = USERSOFPROJECTS;
                    }
                }
            }
            AfterSaveusersofprojects(USERSOFPROJECTS: USERSOFPROJECTS, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteusersofprojects(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteusersofprojects(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                usersofprojects etmp = Getusersofprojects(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.usersofprojects_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.usersofprojects_active = false;
                etmp.deletedusersofprojects_id = true;
                usersofprojects eresulttmp = Saveusersofprojects(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedusersofprojects_id == true)
                    result = true;
            }
            AfterDeleteusersofprojects(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public usersofprojects Getusersofprojects(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            usersofprojects result = new usersofprojects();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<usersofprojects>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.usersofprojects_use == false) || (result.deletedusersofprojects_id == true) || (result.usersofprojects_active==false))
                            result = new usersofprojects();
                }
            }
            return result;
        }
        public List<usersofprojects> GetAllusersofprojects(string whereclause , string DBTYPE , string CONNSTR , bool ALL=false)
        {
            List<usersofprojects> result = new List<usersofprojects>();
            BeforeGetAllusersofprojects(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_usersofprojects info = new info_usersofprojects();
                whereclause += "AND " + info.usersofprojects_deletedusersofprojects_id + " = false AND " + info.usersofprojects_usersofprojects_use + " = true AND " + info.usersofprojects_usersofprojects_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<usersofprojects>(whereclause: whereclause).ToList();
                }            }            AfterGetAllusersofprojects(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveusersofprojects(usersofprojects USERSOFPROJECTS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveusersofprojects(usersofprojects USERSOFPROJECTS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteusersofprojects (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteusersofprojects(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetusersofprojects(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetusersofprojects(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllusersofprojects(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAllusersofprojects(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
