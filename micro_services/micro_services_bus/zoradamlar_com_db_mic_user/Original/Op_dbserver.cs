using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_dbserver
    {
        public dbserver Savedbserver(dbserver DBSERVER, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            dbserver result = new dbserver();
            BeforeSavedbserver(DBSERVER: DBSERVER, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                DBSERVER.dbserver_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                DBSERVER.dbserver_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (DBSERVER.dbserver_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<dbserver>(DBSERVER);
                        if (id != 0)
                            result = db.Get<dbserver>(id);
                    }
                    else
                    {
                        bool ok = db.Update<dbserver>(DBSERVER);
                        if (ok == true)
                            result = db.Get<dbserver>(DBSERVER.dbserver_id);
                        else
                            result = DBSERVER;
                    }
                }
            }
            AfterSavedbserver(DBSERVER: DBSERVER, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletedbserver(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletedbserver(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                dbserver etmp = Getdbserver(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.dbserver_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.dbserver_active = false;
                etmp.deleteddbserver_id = true;
                dbserver eresulttmp = Savedbserver(etmp, ALLOFUSERS);
                if (eresulttmp.deleteddbserver_id == true)
                    result = true;
            }
            AfterDeletedbserver(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public dbserver Getdbserver(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            dbserver result = new dbserver();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<dbserver>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.dbserver_use == false) || (result.deleteddbserver_id == true) || (result.dbserver_active==false))
                            result = new dbserver();
                }
            }
            return result;
        }
        public List<dbserver> GetAlldbserver(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<dbserver> result = new List<dbserver>();
            BeforeGetAlldbserver(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_dbserver info = new info_dbserver();
                whereclause += " AND " + info.dbserver_deleteddbserver_id + " = false AND " + info.dbserver_dbserver_use + " = true AND " + info.dbserver_dbserver_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<dbserver>(whereclause: whereclause).ToList();
                }            }            AfterGetAlldbserver(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavedbserver(dbserver DBSERVER, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSavedbserver(dbserver DBSERVER, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeletedbserver (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeletedbserver(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetdbserver(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetdbserver(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAlldbserver(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAlldbserver(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
