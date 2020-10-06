using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_country
    {
        public country Savecountry(country COUNTRY, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            country result = new country();
            BeforeSavecountry(COUNTRY: COUNTRY, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                COUNTRY.country_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                COUNTRY.country_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
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
            AfterSavecountry(COUNTRY: COUNTRY, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletecountry(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletecountry(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                country etmp = Getcountry(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.country_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.country_active = false;
                etmp.deletedcountry_id = true;
                country eresulttmp = Savecountry(etmp, ALLOFUSERS);
                if (eresulttmp.deletedcountry_id == true)
                    result = true;
            }
            AfterDeletecountry(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public country Getcountry(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            country result = new country();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<country>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.country_use == false) || (result.deletedcountry_id == true) || (result.country_active==false))
                            result = new country();
                }
            }
            return result;
        }
        public List<country> GetAllcountry(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<country> result = new List<country>();
            BeforeGetAllcountry(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_country info = new info_country();
                whereclause += " AND " + info.country_deletedcountry_id + " = false AND " + info.country_country_use + " = true AND " + info.country_country_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<country>(whereclause: whereclause).ToList();
                }            }            AfterGetAllcountry(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavecountry(country COUNTRY, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSavecountry(country COUNTRY, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeletecountry (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeletecountry(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetcountry(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetcountry(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllcountry(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllcountry(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
