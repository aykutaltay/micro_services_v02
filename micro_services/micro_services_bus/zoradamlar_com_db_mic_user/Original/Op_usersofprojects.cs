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
    public partial class Op_usersofprojects
    {
        public usersofprojects Saveusersofprojects(usersofprojects USERSOFPROJECTS, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            usersofprojects result = new usersofprojects();
            BeforeSaveusersofprojects(USERSOFPROJECTS: USERSOFPROJECTS, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERSOFPROJECTS.usersofprojects_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERSOFPROJECTS.usersofprojects_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (USERSOFPROJECTS.usersofprojects_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<usersofprojects>(USERSOFPROJECTS);
                        if (id != 0)
                            result = db.Get<usersofprojects>(id);
                    }
                    else
                    {
                        bool ok = db.Update<usersofprojects>(USERSOFPROJECTS);
                        if (ok == true)
                            result = db.Get<usersofprojects>(USERSOFPROJECTS.usersofprojects_id);
                        else
                            result = USERSOFPROJECTS;
                    }
                }
            }
            AfterSaveusersofprojects(USERSOFPROJECTS: USERSOFPROJECTS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteusersofprojects(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteusersofprojects(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                usersofprojects etmp = Getusersofprojects(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.usersofprojects_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.usersofprojects_active = false;
                etmp.deletedusersofprojects_id = true;
                usersofprojects eresulttmp = Saveusersofprojects(etmp, ALLOFUSERS);
                if (eresulttmp.deletedusersofprojects_id == true)
                    result = true;
            }
            AfterDeleteusersofprojects(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public usersofprojects Getusersofprojects(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            usersofprojects result = new usersofprojects();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<usersofprojects>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.usersofprojects_use == false) || (result.deletedusersofprojects_id == true) || (result.usersofprojects_active==false))
                            result = new usersofprojects();
                }
            }
            return result;
        }
        public List<usersofprojects> GetAllusersofprojects(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<usersofprojects> result = new List<usersofprojects>();
            BeforeGetAllusersofprojects(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_usersofprojects info = new info_usersofprojects();
                whereclause += " AND " + info.usersofprojects_deletedusersofprojects_id + " = false AND " + info.usersofprojects_usersofprojects_use + " = true AND " + info.usersofprojects_usersofprojects_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<usersofprojects>(whereclause: whereclause).ToList();
                }            }            AfterGetAllusersofprojects(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveusersofprojects(usersofprojects USERSOFPROJECTS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaveusersofprojects(usersofprojects USERSOFPROJECTS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleteusersofprojects (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleteusersofprojects(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetusersofprojects(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetusersofprojects(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllusersofprojects(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllusersofprojects(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
        public string Single_crud (cRequest request, allofusers e_aou)
        {
             string result = AppStaticStr.msg0040Hata;
             #region gelen paket içinden yapilacak işlemin bilgilerinin alinmasi
             List<ex_data> l_ed_opt = request.data_ex.Where(w => w.info == AppStaticStr.SrvOpt).ToList();
             if (l_ed_opt == null) return result;
             if (l_ed_opt.Count != 1) return result;
             #endregion gelen paket içinden yapilacak işlemin bilgilerinin alinmasi
             if (l_ed_opt[0].value==AppStaticStr.SingleCrudSave)
                 {
                     usersofprojects ent = JsonConvert.DeserializeObject<usersofprojects>(request.data);
                     usersofprojects save_ent = Saveusersofprojects(ent, e_aou, false, false);
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
                 usersofprojects ent = JsonConvert.DeserializeObject<usersofprojects>(request.data);
                 bool resu = Deleteusersofprojects (ID: ent.usersofprojects_id, ALLOFUSERS: e_aou, SYNC: false, TRAN: false);
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
                 usersofprojects ent = Getusersofprojects(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.usersofprojects_id==ID)
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
                 List<usersofprojects> ent = GetAllusersofprojects(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
    }

}
