using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_passofusers
    {
        public passofusers Savepassofusers(passofusers PASSOFUSERS, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            passofusers result = new passofusers();
            BeforeSavepassofusers(PASSOFUSERS: PASSOFUSERS, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PASSOFUSERS.passofusers_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PASSOFUSERS.passofusers_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (PASSOFUSERS.passofusers_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<passofusers>(PASSOFUSERS);
                        if (id != 0)
                            result = db.Get<passofusers>(id);
                    }
                    else
                    {
                        bool ok = db.Update<passofusers>(PASSOFUSERS);
                        if (ok == true)
                            result = db.Get<passofusers>(PASSOFUSERS.passofusers_id);
                        else
                            result = PASSOFUSERS;
                    }
                }
            }
            AfterSavepassofusers(PASSOFUSERS: PASSOFUSERS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletepassofusers(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletepassofusers(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                passofusers etmp = Getpassofusers(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.passofusers_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.passofusers_active = false;
                etmp.deletedpassofusers_id = true;
                passofusers eresulttmp = Savepassofusers(etmp, ALLOFUSERS);
                if (eresulttmp.deletedpassofusers_id == true)
                    result = true;
            }
            AfterDeletepassofusers(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public passofusers Getpassofusers(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            passofusers result = new passofusers();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<passofusers>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.passofusers_use == false) || (result.deletedpassofusers_id == true) || (result.passofusers_active==false))
                            result = new passofusers();
                }
            }
            return result;
        }
        public List<passofusers> GetAllpassofusers(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<passofusers> result = new List<passofusers>();
            BeforeGetAllpassofusers(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_passofusers info = new info_passofusers();
                whereclause += " AND " + info.passofusers_deletedpassofusers_id + " = false AND " + info.passofusers_passofusers_use + " = true AND " + info.passofusers_passofusers_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<passofusers>(whereclause: whereclause).ToList();
                }            }            AfterGetAllpassofusers(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavepassofusers(passofusers PASSOFUSERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSavepassofusers(passofusers PASSOFUSERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeletepassofusers (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeletepassofusers(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetpassofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetpassofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllpassofusers(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllpassofusers(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
