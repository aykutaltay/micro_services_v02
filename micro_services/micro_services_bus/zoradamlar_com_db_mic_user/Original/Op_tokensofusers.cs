using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_tokensofusers
    {
        public tokensofusers Savetokensofusers(tokensofusers TOKENSOFUSERS, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            tokensofusers result = new tokensofusers();
            BeforeSavetokensofusers(TOKENSOFUSERS: TOKENSOFUSERS,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                TOKENSOFUSERS.tokensofusers_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                TOKENSOFUSERS.tokensofusers_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
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
            AfterSavetokensofusers(TOKENSOFUSERS: TOKENSOFUSERS, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletetokensofusers(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletetokensofusers(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                tokensofusers etmp = Gettokensofusers(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.tokensofusers_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.tokensofusers_active = false;
                etmp.deletedtokensofusers_id = true;
                tokensofusers eresulttmp = Savetokensofusers(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedtokensofusers_id == true)
                    result = true;
            }
            AfterDeletetokensofusers(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public tokensofusers Gettokensofusers(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            tokensofusers result = new tokensofusers();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<tokensofusers>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.tokensofusers_use == false) || (result.deletedtokensofusers_id == true) || (result.tokensofusers_active==false))
                            result = new tokensofusers();
                }
            }
            return result;
        }
        public List<tokensofusers> GetAllusers(string whereclause = "1 = 1", string DBTYPE = " ", string CONNSTR = " ", bool ALL=false)
        {
            List<tokensofusers> result = new List<tokensofusers>();
            BeforeGetAlltokensofusers(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_tokensofusers info = new info_tokensofusers();
                whereclause += "AND " + info.tokensofusers_deletedtokensofusers_id + " = false AND " + info.tokensofusers_tokensofusers_use + " = true AND " + info.tokensofusers_tokensofusers_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<tokensofusers>(whereclause: whereclause).ToList();
                }            }            AfterGetAlltokensofusers(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSavetokensofusers(tokensofusers TOKENSOFUSERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSavetokensofusers(tokensofusers TOKENSOFUSERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeletetokensofusers (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeletetokensofusers(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGettokensofusers(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGettokensofusers(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAlltokensofusers(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAlltokensofusers(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
