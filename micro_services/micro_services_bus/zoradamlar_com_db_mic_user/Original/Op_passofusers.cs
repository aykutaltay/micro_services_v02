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
    public partial class Op_passofusers
    {
        public passofusers Savepassofusers(passofusers PASSOFUSERS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            string connstr = GetConnStr(ALLOFUSERS);
            passofusers result = new passofusers();
            BeforeSavepassofusers(PASSOFUSERS: PASSOFUSERS, ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PASSOFUSERS.passofusers_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PASSOFUSERS.passofusers_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                if (DB_MYSQL == null)
                {
                   using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
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
                else
                {
                   Mysql_dapper db = DB_MYSQL;
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
            AfterSavepassofusers(PASSOFUSERS: PASSOFUSERS, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletepassofusers(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletepassofusers(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
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
                passofusers eresulttmp = Savepassofusers(PASSOFUSERS:etmp, ALLOFUSERS:ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC,TRAN:TRAN);
                if (eresulttmp.deletedpassofusers_id == true)
                    result = true;
            }
            AfterDeletepassofusers(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            return result;
        }
        public passofusers Getpassofusers(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            passofusers result = new passofusers();
            string connstr = GetConnStr(ALLOFUSERS);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
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
            string connstr = GetConnStr(ALLOFUSERS);
            BeforeGetAllpassofusers(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_passofusers info = new info_passofusers();
                whereclause += " AND " + info.passofusers_deletedpassofusers_id + " = false AND " + info.passofusers_passofusers_use + " = true AND " + info.passofusers_passofusers_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr, usetransaction: false))
                {
                    result = db.GetAll<passofusers>(whereclause: whereclause).ToList();
                }            }            AfterGetAllpassofusers(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavepassofusers(passofusers PASSOFUSERS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterSavepassofusers(passofusers PASSOFUSERS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterDeletepassofusers (long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeDeletepassofusers(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeGetpassofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetpassofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllpassofusers(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllpassofusers(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     passofusers ent = JsonConvert.DeserializeObject<passofusers>(request.data);
                     passofusers save_ent = Savepassofusers(ent, e_aou, DB_MYSQL, false, false);
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
                 passofusers ent = JsonConvert.DeserializeObject<passofusers>(request.data);
                 bool resu = Deletepassofusers (ID: ent.passofusers_id, ALLOFUSERS: e_aou, DB_MYSQL:DB_MYSQL ,SYNC: false, TRAN: false);
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
                 passofusers ent = Getpassofusers(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.passofusers_id==ID)
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
                 List<passofusers> ent = GetAllpassofusers(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
