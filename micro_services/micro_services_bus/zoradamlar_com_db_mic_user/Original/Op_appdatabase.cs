using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_appdatabase
    {
        public appdatabase Saveappdatabase(appdatabase APPDATABASE, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            appdatabase result = new appdatabase();
            BeforeSaveappdatabase(APPDATABASE: APPDATABASE,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                APPDATABASE.appdatabase_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                APPDATABASE.appdatabase_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (APPDATABASE.appdatabase_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<appdatabase>(APPDATABASE);
                        if (id != 0)
                            result = db.Get<appdatabase>(id);
                    }
                    else
                    {
                        bool ok = db.Update<appdatabase>(APPDATABASE);
                        if (ok == true)
                            result = db.Get<appdatabase>(APPDATABASE.appdatabase_id);
                        else
                            result = APPDATABASE;
                    }
                }
            }
            AfterSaveappdatabase(APPDATABASE: APPDATABASE, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteappdatabase(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteappdatabase(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                appdatabase etmp = Getappdatabase(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.appdatabase_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.appdatabase_active = false;
                etmp.deletedappdatabase_id = true;
                appdatabase eresulttmp = Saveappdatabase(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedappdatabase_id == true)
                    result = true;
            }
            AfterDeleteappdatabase(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public appdatabase Getappdatabase(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            appdatabase result = new appdatabase();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<appdatabase>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.appdatabase_use == false) || (result.deletedappdatabase_id == true) || (result.appdatabase_active==false))
                            result = new appdatabase();
                }
            }
            return result;
        }
        public List<appdatabase> GetAllusers(string whereclause = "1 = 1", string DBTYPE = " ", string CONNSTR = " ", bool ALL=false)
        {
            List<appdatabase> result = new List<appdatabase>();
            BeforeGetAllappdatabase(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_appdatabase info = new info_appdatabase();
                whereclause += "AND " + info.appdatabase_deletedappdatabase_id + " = false AND " + info.appdatabase_appdatabase_use + " = true AND " + info.appdatabase_appdatabase_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<appdatabase>(whereclause: whereclause).ToList();
                }            }            AfterGetAllappdatabase(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveappdatabase(appdatabase APPDATABASE, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveappdatabase(appdatabase APPDATABASE, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteappdatabase (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteappdatabase(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetappdatabase(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetappdatabase(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllappdatabase(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAllappdatabase(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
