using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_share.Model;
using Newtonsoft.Json;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_company
    {
        public company Savecompany(company COMPANY, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            string connstr = GetConnStr(ALLOFUSERS);
            company result = new company();
            BeforeSavecompany(COMPANY: COMPANY, ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                COMPANY.company_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                COMPANY.company_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                if (DB_MYSQL == null)
                {
                   using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
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
                else
                {
                   Mysql_dapper db = DB_MYSQL;
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
            AfterSavecompany(COMPANY: COMPANY, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletecompany(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletecompany(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
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
                company eresulttmp = Savecompany(COMPANY:etmp, ALLOFUSERS:ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC,TRAN:TRAN);
                if (eresulttmp.deletedcompany_id == true)
                    result = true;
            }
            AfterDeletecompany(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            return result;
        }
        public company Getcompany(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            company result = new company();
            string connstr = GetConnStr(ALLOFUSERS);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
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
            string connstr = GetConnStr(ALLOFUSERS);
            BeforeGetAllcompany(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_company info = new info_company();
                whereclause += " AND " + info.company_deletedcompany_id + " = false AND " + info.company_company_use + " = true AND " + info.company_company_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr, usetransaction: false))
                {
                    result = db.GetAll<company>(whereclause: whereclause).ToList();
                }            }            AfterGetAllcompany(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavecompany(company COMPANY, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterSavecompany(company COMPANY, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterDeletecompany (long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeDeletecompany(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeGetcompany(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetcompany(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllcompany(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllcompany(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
        public string Single_crud (cRequest request, allofusers e_aou, Mysql_dapper DB_MYSQL=null)
        {
             string result = AppStaticStr.msg0040Hata;
             #region gelen paket içinden yapilacak işlemin bilgilerinin alinmasi
             List<ex_data> l_ed_opt = request.data_ex.Where(w => w.info == AppStaticStr.SrvOpt).ToList();
             if (l_ed_opt == null) return result;
             if (l_ed_opt.Count != 1) return result;
             #endregion gelen paket içinden yapilacak işlemin bilgilerinin alinmasi
             if (l_ed_opt[0].value==AppStaticStr.SingleCrudSave)
                 {
                     company ent = JsonConvert.DeserializeObject<company>(request.data);
                     company save_ent = Savecompany(ent, e_aou, DB_MYSQL, false, false);
                     cResponse res = new cResponse()
                     {
                         message_code = AppStaticInt.msg001Succes,
                         message = AppStaticStr.msg0045OK,
                         data = JsonConvert.SerializeObject(save_ent),
                         token = request.token
                     };
                     result = JsonConvert.SerializeObject(res);
                 }
             if (l_ed_opt[0].value==AppStaticStr.SingleCrudDelete)
             {
                 company ent = JsonConvert.DeserializeObject<company>(request.data);
                 bool resu = Deletecompany (ID: ent.company_id, ALLOFUSERS: e_aou, DB_MYSQL:DB_MYSQL ,SYNC: false, TRAN: false);
                 if (resu == true)
                 {
                     cResponse res = new cResponse()
                     {
                         message_code = AppStaticInt.msg001Succes,
                         message = AppStaticStr.msg0045OK,
                         data = resu.ToString(),
                         token = request.token
                     };
                     result = JsonConvert.SerializeObject(res);
                 }
             }
             if (l_ed_opt[0].value==AppStaticStr.SingleCrudGet)
             {
                 int ID = 0;
                 int.TryParse(request.data, out ID);
                 company ent = Getcompany(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.company_id==ID)
                     {
                         cResponse res = new cResponse()
                         {
                         message_code = AppStaticInt.msg001Succes,
                         data = JsonConvert.SerializeObject(ent),
                         token = request.token
                         };
                         result = JsonConvert.SerializeObject(res);
                     }
                 }
             }
             if (l_ed_opt[0].value==AppStaticStr.SingleCrudGetAll)
             {
                 List<company> ent = GetAllcompany(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
                 if (ent != null)
                 {
                     if (ent.Count>0)
                     {
                         cResponse res = new cResponse()
                         {
                             message_code = AppStaticInt.msg001Succes,
                             message = AppStaticStr.msg0045OK,
                             data = JsonConvert.SerializeObject(ent),
                             token = request.token
                         };
                         result = JsonConvert.SerializeObject(res);
                     }
                 }
             }
             return result;
        }
        public string GetConnStr (allofusers ALLOFUSERS)

        {
            string result = string.Empty;
            if (ALLOFUSERS.projects_id == AppStaticInt.ProjectCodeCore)
                result = ALLOFUSERS.appdatabase_connstr;
            long db_ID = 0;
            long.TryParse(ALLOFUSERS.company_dbserver_id.ToString(), out db_ID);
            if (db_ID == 0)
                result = ALLOFUSERS.appdatabase_connstr;
            else
                result = ALLOFUSERS.dbserver_adrr;
            return result;
        }
    }

}
