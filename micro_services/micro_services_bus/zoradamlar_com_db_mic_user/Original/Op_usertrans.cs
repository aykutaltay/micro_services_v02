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
    public partial class Op_usertrans
    {
        public usertrans Saveusertrans(usertrans USERTRANS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            string connstr = GetConnStr(ALLOFUSERS);
            usertrans result = new usertrans();
            BeforeSaveusertrans(USERTRANS: USERTRANS, ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERTRANS.usertrans_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERTRANS.usertrans_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                if (DB_MYSQL == null)
                {
                   using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
                   {
                        if (USERTRANS.usertrans_id == 0)
                       {
                            long id = 0;
                            id = db.Insert<usertrans>(USERTRANS);
                           if (id != 0)
                             result = db.Get<usertrans>(id);
                       }
                       else
                       {
                         bool ok = db.Update<usertrans>(USERTRANS);
                           if (ok == true)
                             result = db.Get<usertrans>(USERTRANS.usertrans_id);
                           else
                             result = USERTRANS;
                       }
                   }
                }
                else
                {
                   Mysql_dapper db = DB_MYSQL;
                        if (USERTRANS.usertrans_id == 0)
                       {
                            long id = 0;
                            id = db.Insert<usertrans>(USERTRANS);
                           if (id != 0)
                             result = db.Get<usertrans>(id);
                       }
                       else
                       {
                         bool ok = db.Update<usertrans>(USERTRANS);
                           if (ok == true)
                             result = db.Get<usertrans>(USERTRANS.usertrans_id);
                           else
                             result = USERTRANS;
                       }
                }
            }
            AfterSaveusertrans(USERTRANS: USERTRANS, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteusertrans(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteusertrans(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                usertrans etmp = Getusertrans(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.usertrans_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.usertrans_active = false;
                etmp.deletedusertrans_id = true;
                usertrans eresulttmp = Saveusertrans(USERTRANS:etmp, ALLOFUSERS:ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC,TRAN:TRAN);
                if (eresulttmp.deletedusertrans_id == true)
                    result = true;
            }
            AfterDeleteusertrans(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            return result;
        }
        public usertrans Getusertrans(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            usertrans result = new usertrans();
            string connstr = GetConnStr(ALLOFUSERS);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
                {                    result = db.Get<usertrans>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.usertrans_use == false) || (result.deletedusertrans_id == true) || (result.usertrans_active==false))
                            result = new usertrans();
                }
            }
            return result;
        }
        public List<usertrans> GetAllusertrans(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<usertrans> result = new List<usertrans>();
            string connstr = GetConnStr(ALLOFUSERS);
            BeforeGetAllusertrans(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_usertrans info = new info_usertrans();
                whereclause += " AND " + info.usertrans_deletedusertrans_id + " = false AND " + info.usertrans_usertrans_use + " = true AND " + info.usertrans_usertrans_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr, usetransaction: false))
                {
                    result = db.GetAll<usertrans>(whereclause: whereclause).ToList();
                }            }            AfterGetAllusertrans(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveusertrans(usertrans USERTRANS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterSaveusertrans(usertrans USERTRANS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterDeleteusertrans (long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeDeleteusertrans(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeGetusertrans(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetusertrans(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllusertrans(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllusertrans(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     usertrans ent = JsonConvert.DeserializeObject<usertrans>(request.data);
                     usertrans save_ent = Saveusertrans(ent, e_aou, DB_MYSQL, false, false);
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
                 usertrans ent = JsonConvert.DeserializeObject<usertrans>(request.data);
                 bool resu = Deleteusertrans (ID: ent.usertrans_id, ALLOFUSERS: e_aou, DB_MYSQL:DB_MYSQL ,SYNC: false, TRAN: false);
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
                 usertrans ent = Getusertrans(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.usertrans_id==ID)
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
                 List<usertrans> ent = GetAllusertrans(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
             if (l_ed_opt[0].value==AppStaticStr.SingleCrudGetAll_true)
             {
                 List<usertrans> ent = GetAllusertrans(whereclause:request.data, ALLOFUSERS: e_aou, ALL: true);
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
