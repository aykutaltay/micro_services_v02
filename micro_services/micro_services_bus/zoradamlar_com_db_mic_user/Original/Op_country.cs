using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_country
    {
        public country Savecountry(country COUNTRY, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            country result = new country();
            BeforeSavecountry(COUNTRY: COUNTRY,DBTYPE: DBTYPE, CONNSTR:CONNSTR, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                COUNTRY.country_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                COUNTRY.country_active = false;
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    if (COUNTRY.country_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<country>(COUNTRY);
                        if (id != 0)
                            result = db.Get<country>(id);
                    }
                    else
                    {
                        bool ok = db.Update<country>(COUNTRY);
                        if (ok == true)
                            result = db.Get<country>(COUNTRY.country_id);
                        else
                            result = COUNTRY;
                    }
                }
            }
            AfterSavecountry(COUNTRY: COUNTRY, DBTYPE: DBTYPE, CONNSTR: CONNSTR, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletecountry(long ID, string DBTYPE, string CONNSTR, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletecountry(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                country etmp = Getcountry(ID, DBTYPE, CONNSTR);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.country_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.country_active = false;
                etmp.deletedcountry_id = true;
                country eresulttmp = Savecountry(etmp, DBTYPE, CONNSTR);
                if (eresulttmp.deletedcountry_id == true)
                    result = true;
            }
            AfterDeletecountry(ID, DBTYPE, CONNSTR, SYNC, TRAN);
            return result;
        }
        public country Getcountry(long ID, string DBTYPE, string CONNSTR, bool ALL=false)
        {
            country result = new country();
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {                    result = db.Get<country>(id: ID);
                    //senkron dışında ve silinenlerin dışındakileri getirmesi
                    if (ALL==false)
                        if ((result.country_use == false) || (result.deletedcountry_id == true) || (result.country_active==false))
                            result = new country();
                }
            }
            return result;
        }
        public List<country> GetAllusers(string whereclause = "1 = 1", string DBTYPE = " ", string CONNSTR = " ", bool ALL=false)
        {
            List<country> result = new List<country>();
            BeforeGetAllcountry(whereclause, DBTYPE, CONNSTR, ALL);
            //senkron dışında ve silinenlerin dışındakileri getirmesi
            if (ALL == false)
            {
                info_country info = new info_country();
                whereclause += "AND " + info.country_deletedcountry_id + " = false AND " + info.country_country_use + " = true AND " + info.country_country_active + " = true";
            }
            if (DBTYPE == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: CONNSTR, usetransaction: false))
                {
                    result = db.GetAll<country>(whereclause: whereclause).ToList();
                }            }            AfterGetAllcountry(whereclause, DBTYPE, CONNSTR, ALL);
            return result;
        }
        public void BeforeSavecountry(country COUNTRY, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterSavecountry(country COUNTRY, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void AfterDeletecountry (long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeDeletecountry(long ID, string DBTYPE, string CONNSTR, bool SYNC, bool TRAN) { }
        public void BeforeGetcountry(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void AfterGetcountry(long ID, string DBTYPE, string CONNSTR, bool ALL) { }
        public void BeforeGetAllcountry(string whereclause , string DBTYPE , string CONNSTR , bool ALL ) { }
        public void AfterGetAllcountry(string whereclause, string DBTYPE, string CONNSTR, bool ALL) { }
    }

}
