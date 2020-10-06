using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_company
    {
        public company Savecompany(company COMPANY, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            company result = new company();
            BeforeSavecompany(COMPANY: COMPANY, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                COMPANY.company_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                COMPANY.company_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (COMPANY.company_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<company>(COMPANY);
                        if (id != 0)
                            result = db.Get<company>(id);
                    }
                    else
                    {
                        bool ok = db.Update<company>(COMPANY);
                        if (ok == true)
                            result = db.Get<company>(COMPANY.company_id);
                        else
                            result = COMPANY;
                    }
                }
            }
            AfterSavecompany(COMPANY: COMPANY, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletecompany(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletecompany(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                company etmp = Getcompany(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.company_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.company_active = false;
                etmp.deletedcompany_id = true;
                company eresulttmp = Savecompany(etmp, ALLOFUSERS);
                if (eresulttmp.deletedcompany_id == true)
                    result = true;
            }
            AfterDeletecompany(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public company Getcompany(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            company result = new company();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<company>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.company_use == false) || (result.deletedcompany_id == true) || (result.company_active==false))
                            result = new company();
                }
            }
            return result;
        }
        public List<company> GetAllcompany(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<company> result = new List<company>();
            BeforeGetAllcompany(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_company info = new info_company();
                whereclause += " AND " + info.company_deletedcompany_id + " = false AND " + info.company_company_use + " = true AND " + info.company_company_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<company>(whereclause: whereclause).ToList();
                }            }            AfterGetAllcompany(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavecompany(company COMPANY, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSavecompany(company COMPANY, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeletecompany (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeletecompany(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetcompany(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetcompany(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllcompany(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllcompany(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
