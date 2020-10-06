using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_tokensofusers
    {
        public tokensofusers Savetokensofusers(tokensofusers TOKENSOFUSERS, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            tokensofusers result = new tokensofusers();
            BeforeSavetokensofusers(TOKENSOFUSERS: TOKENSOFUSERS, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                TOKENSOFUSERS.tokensofusers_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                TOKENSOFUSERS.tokensofusers_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (TOKENSOFUSERS.tokensofusers_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<tokensofusers>(TOKENSOFUSERS);
                        if (id != 0)
                            result = db.Get<tokensofusers>(id);
                    }
                    else
                    {
                        bool ok = db.Update<tokensofusers>(TOKENSOFUSERS);
                        if (ok == true)
                            result = db.Get<tokensofusers>(TOKENSOFUSERS.tokensofusers_id);
                        else
                            result = TOKENSOFUSERS;
                    }
                }
            }
            AfterSavetokensofusers(TOKENSOFUSERS: TOKENSOFUSERS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletetokensofusers(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletetokensofusers(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                tokensofusers etmp = Gettokensofusers(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.tokensofusers_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.tokensofusers_active = false;
                etmp.deletedtokensofusers_id = true;
                tokensofusers eresulttmp = Savetokensofusers(etmp, ALLOFUSERS);
                if (eresulttmp.deletedtokensofusers_id == true)
                    result = true;
            }
            AfterDeletetokensofusers(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public tokensofusers Gettokensofusers(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            tokensofusers result = new tokensofusers();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<tokensofusers>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.tokensofusers_use == false) || (result.deletedtokensofusers_id == true) || (result.tokensofusers_active==false))
                            result = new tokensofusers();
                }
            }
            return result;
        }
        public List<tokensofusers> GetAlltokensofusers(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<tokensofusers> result = new List<tokensofusers>();
            BeforeGetAlltokensofusers(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_tokensofusers info = new info_tokensofusers();
                whereclause += " AND " + info.tokensofusers_deletedtokensofusers_id + " = false AND " + info.tokensofusers_tokensofusers_use + " = true AND " + info.tokensofusers_tokensofusers_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<tokensofusers>(whereclause: whereclause).ToList();
                }            }            AfterGetAlltokensofusers(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavetokensofusers(tokensofusers TOKENSOFUSERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSavetokensofusers(tokensofusers TOKENSOFUSERS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeletetokensofusers (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeletetokensofusers(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGettokensofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGettokensofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAlltokensofusers(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAlltokensofusers(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
