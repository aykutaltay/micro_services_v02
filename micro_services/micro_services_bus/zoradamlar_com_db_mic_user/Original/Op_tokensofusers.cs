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
    public partial class Op_tokensofusers
    {
        public tokensofusers Savetokensofusers(tokensofusers TOKENSOFUSERS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            string connstr = GetConnStr(ALLOFUSERS);
            tokensofusers result = new tokensofusers();
            BeforeSavetokensofusers(TOKENSOFUSERS: TOKENSOFUSERS, ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                TOKENSOFUSERS.tokensofusers_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                TOKENSOFUSERS.tokensofusers_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                if (DB_MYSQL == null)
                {
                   using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
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
                else
                {
                   Mysql_dapper db = DB_MYSQL;
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
            AfterSavetokensofusers(TOKENSOFUSERS: TOKENSOFUSERS, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deletetokensofusers(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeletetokensofusers(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
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
                tokensofusers eresulttmp = Savetokensofusers(TOKENSOFUSERS:etmp, ALLOFUSERS:ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC,TRAN:TRAN);
                if (eresulttmp.deletedtokensofusers_id == true)
                    result = true;
            }
            AfterDeletetokensofusers(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            return result;
        }
        public tokensofusers Gettokensofusers(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            tokensofusers result = new tokensofusers();
            string connstr = GetConnStr(ALLOFUSERS);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
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
            string connstr = GetConnStr(ALLOFUSERS);
            BeforeGetAlltokensofusers(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_tokensofusers info = new info_tokensofusers();
                whereclause += " AND " + info.tokensofusers_deletedtokensofusers_id + " = false AND " + info.tokensofusers_tokensofusers_use + " = true AND " + info.tokensofusers_tokensofusers_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr, usetransaction: false))
                {
                    result = db.GetAll<tokensofusers>(whereclause: whereclause).ToList();
                }            }            AfterGetAlltokensofusers(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSavetokensofusers(tokensofusers TOKENSOFUSERS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterSavetokensofusers(tokensofusers TOKENSOFUSERS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterDeletetokensofusers (long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeDeletetokensofusers(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeGettokensofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGettokensofusers(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAlltokensofusers(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAlltokensofusers(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     tokensofusers ent = JsonConvert.DeserializeObject<tokensofusers>(request.data);
                     tokensofusers save_ent = Savetokensofusers(ent, e_aou, DB_MYSQL, false, false);
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
                 tokensofusers ent = JsonConvert.DeserializeObject<tokensofusers>(request.data);
                 bool resu = Deletetokensofusers (ID: ent.tokensofusers_id, ALLOFUSERS: e_aou, DB_MYSQL:DB_MYSQL ,SYNC: false, TRAN: false);
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
                 tokensofusers ent = Gettokensofusers(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.tokensofusers_id==ID)
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
                 List<tokensofusers> ent = GetAlltokensofusers(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
