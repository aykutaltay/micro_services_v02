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
    public partial class Op_appserver
    {
        public appserver Saveappserver(appserver APPSERVER, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            string connstr = GetConnStr(ALLOFUSERS);
            appserver result = new appserver();
            BeforeSaveappserver(APPSERVER: APPSERVER, ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                APPSERVER.appserver_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                APPSERVER.appserver_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                if (DB_MYSQL == null)
                {
                   using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
                   {
                        if (APPSERVER.appserver_id == 0)
                       {
                            long id = 0;
                            id = db.Insert<appserver>(APPSERVER);
                           if (id != 0)
                             result = db.Get<appserver>(id);
                       }
                       else
                       {
                         bool ok = db.Update<appserver>(APPSERVER);
                           if (ok == true)
                             result = db.Get<appserver>(APPSERVER.appserver_id);
                           else
                             result = APPSERVER;
                       }
                   }
                }
                else
                {
                   Mysql_dapper db = DB_MYSQL;
                        if (APPSERVER.appserver_id == 0)
                       {
                            long id = 0;
                            id = db.Insert<appserver>(APPSERVER);
                           if (id != 0)
                             result = db.Get<appserver>(id);
                       }
                       else
                       {
                         bool ok = db.Update<appserver>(APPSERVER);
                           if (ok == true)
                             result = db.Get<appserver>(APPSERVER.appserver_id);
                           else
                             result = APPSERVER;
                       }
                }
            }
            AfterSaveappserver(APPSERVER: APPSERVER, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteappserver(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteappserver(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                appserver etmp = Getappserver(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.appserver_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.appserver_active = false;
                etmp.deletedappserver_id = true;
                appserver eresulttmp = Saveappserver(APPSERVER:etmp, ALLOFUSERS:ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC,TRAN:TRAN);
                if (eresulttmp.deletedappserver_id == true)
                    result = true;
            }
            AfterDeleteappserver(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            return result;
        }
        public appserver Getappserver(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            appserver result = new appserver();
            string connstr = GetConnStr(ALLOFUSERS);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
                {                    result = db.Get<appserver>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.appserver_use == false) || (result.deletedappserver_id == true) || (result.appserver_active==false))
                            result = new appserver();
                }
            }
            return result;
        }
        public List<appserver> GetAllappserver(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<appserver> result = new List<appserver>();
            string connstr = GetConnStr(ALLOFUSERS);
            BeforeGetAllappserver(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_appserver info = new info_appserver();
                whereclause += " AND " + info.appserver_deletedappserver_id + " = false AND " + info.appserver_appserver_use + " = true AND " + info.appserver_appserver_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr, usetransaction: false))
                {
                    result = db.GetAll<appserver>(whereclause: whereclause).ToList();
                }            }            AfterGetAllappserver(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveappserver(appserver APPSERVER, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterSaveappserver(appserver APPSERVER, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterDeleteappserver (long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeDeleteappserver(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeGetappserver(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetappserver(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllappserver(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllappserver(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     appserver ent = JsonConvert.DeserializeObject<appserver>(request.data);
                     appserver save_ent = Saveappserver(ent, e_aou, DB_MYSQL, false, false);
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
                 appserver ent = JsonConvert.DeserializeObject<appserver>(request.data);
                 bool resu = Deleteappserver (ID: ent.appserver_id, ALLOFUSERS: e_aou, DB_MYSQL:DB_MYSQL ,SYNC: false, TRAN: false);
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
                 appserver ent = Getappserver(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.appserver_id==ID)
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
                 List<appserver> ent = GetAllappserver(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
