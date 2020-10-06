using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_lang
    {
        public lang Savelang(lang LANG, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            lang result = new lang();
            BeforeSavelang(LANG: LANG, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                LANG.lang_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                LANG.lang_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (LANG.lang_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<lang>(LANG);
                        if (id != 0)
                            result = db.Get<lang>(id);
                    }
                    else
                    {
                        bool ok = db.Update<lang>(LANG);
                        if (ok == true)
                            result = db.Get<lang>(LANG.lang_id);
                        else
                            result = LANG;
                    }
                }
            }
            AfterSavelang(LANG: LANG, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletelang(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletelang(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                lang etmp = Getlang(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.lang_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.lang_active = false;
                etmp.deletedlang_id = true;
                lang eresulttmp = Savelang(etmp, ALLOFUSERS);
                if (eresulttmp.deletedlang_id == true)
                    result = true;
            }
            AfterDeletelang(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public lang Getlang(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            lang result = new lang();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<lang>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.lang_use == false) || (result.deletedlang_id == true) || (result.lang_active==false))
                            result = new lang();
                }
            }
            return result;
        }
        public List<lang> GetAlllang(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<lang> result = new List<lang>();
            BeforeGetAlllang(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_lang info = new info_lang();
                whereclause += " AND " + info.lang_deletedlang_id + " = false AND " + info.lang_lang_use + " = true AND " + info.lang_lang_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<lang>(whereclause: whereclause).ToList();
                }            }            AfterGetAlllang(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavelang(lang LANG, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSavelang(lang LANG, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeletelang (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeletelang(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetlang(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetlang(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAlllang(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAlllang(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
