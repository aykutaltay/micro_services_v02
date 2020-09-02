using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_appserver
    {
        public appserver Saveappserver(appserver APPSERVER, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            appserver result = new appserver();
            BeforeSaveappserver(APPSERVER: APPSERVER,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                APPSERVER.appserver_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                APPSERVER.appserver_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (APPSERVER.appserver_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<appserver>(APPSERVER);
                        if (id != 0)
                            result = db.Get<appserver>(id);
                    }
                    else
                    {
                        bool ok = db.Update<appserver>(APPSERVER);
                        if (ok == true)
                            result = db.Get<appserver>(APPSERVER.appserver_id);
                        else
                            result = APPSERVER;
                    }
                }
            }
            AfterSaveappserver(APPSERVER: APPSERVER, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteappserver(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteappserver(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                appserver etmp = Getappserver(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.appserver_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.appserver_active = false;
                etmp.deletedappserver_id = true;
                appserver eresulttmp = Saveappserver(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedappserver_id == true)
                    result = true;
            }
            AfterDeleteappserver(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public appserver Getappserver(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            appserver result = new appserver();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<appserver>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.appserver_use == false) || (result.deletedappserver_id == true) || (result.appserver_active==false))
                            result = new appserver();
                }
            }
            return result;
        }
        public List<appserver> GetAllusers(string whereclause = "1 = 1", string DBTYPE = " ", string CONNSTR = " ", bool ALL=false)
        {
            List<appserver> result = new List<appserver>();
            BeforeGetAllappserver(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_appserver info = new info_appserver();
                whereclause += "AND " + info.appserver_deletedappserver_id + " = false AND " + info.appserver_appserver_use + " = true AND " + info.appserver_appserver_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<appserver>(whereclause: whereclause).ToList();
                }            }            AfterGetAllappserver(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveappserver(appserver APPSERVER, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveappserver(appserver APPSERVER, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteappserver (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteappserver(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetappserver(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetappserver(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllappserver(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAllappserver(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
