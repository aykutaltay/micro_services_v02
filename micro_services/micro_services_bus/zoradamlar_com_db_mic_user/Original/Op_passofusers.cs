using System;
using System.Collections.Generic;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using System.Text;
using micro_services_share;
using micro_services_dal;
using System.Linq;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_passofusers
    {
        public passofusers Savepassofusers(passofusers PASSOFUSERS, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            passofusers result = new passofusers();
            BeforeSaveusers(PASSOFUSERS: PASSOFUSERS, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);

            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PASSOFUSERS.passofusers_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PASSOFUSERS.passofusers_active = false;

            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
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

            AfterSaveusers(PASSOFUSERS: PASSOFUSERS, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);

            return result;

        }
        public bool Deletepassofusers(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;

            BeforeDeleteusers(ID, DBTYPE, CONNSTR, SYNC, TRAN);

            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                passofusers etmp = Getpassofusers(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.passofusers_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.passofusers_active = false;

                etmp.deletedpassofusers_id = true;
                passofusers eresulttmp = Savepassofusers(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedpassofusers_id == true)
                    result = true;
            }

            AfterDeleteusers(ID, DBTYPE, CONNSTR, SYNC, TRAN);

            return result;
        }
        public passofusers Getpassofusers(long ID, string DBTYPE, string CONNSTR, bool ALL = false)
        {
            passofusers result = new passofusers();

            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.Get<passofusers>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL == false)
                        if ((result.passofusers_use == false) || (result.deletedpassofusers_id == true) || (result.passofusers_active == false))
                            result = new passofusers();
                }
            }

            return result;
        }
        public List<passofusers> GetAllpassofusers(string whereclause = "1=1", string DBTYPE = "", string CONNSTR = "", bool ALL = false)
        {
            //var param = JsonConvert.DeserializeAnonymousType(PARAMETERS, new { p = 1 });

            List<passofusers> result = new List<passofusers>();

            BeforeGetAllusers(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_passofusers info = new info_passofusers();
                whereclause += string.Format(" AND {0}=false AND {1}=true AND {2}=true", info.passofusers_deletedpassofusers_id, info.passofusers_passofusers_use, info.passofusers_passofusers_active);
            }

            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<passofusers>(whereclause: whereclause).ToList();
                   
                }
            }
            AfterGetAllusers(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSaveusers(passofusers PASSOFUSERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSaveusers(passofusers PASSOFUSERS, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeleteusers(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeleteusers(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetusers(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetusers(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllusers(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetAllusers(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }
}
