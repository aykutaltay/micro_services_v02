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
    public partial class Op_projects
    {
        public projects Saveprojects(projects PROJECTS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            string connstr = GetConnStr(ALLOFUSERS);
            projects result = new projects();
            BeforeSaveprojects(PROJECTS: PROJECTS, ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PROJECTS.projects_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PROJECTS.projects_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                if (DB_MYSQL == null)
                {
                   using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
                   {
                        if (PROJECTS.projects_id == 0)
                       {
                            long id = 0;
                            id = db.Insert<projects>(PROJECTS);
                           if (id != 0)
                             result = db.Get<projects>(id);
                       }
                       else
                       {
                         bool ok = db.Update<projects>(PROJECTS);
                           if (ok == true)
                             result = db.Get<projects>(PROJECTS.projects_id);
                           else
                             result = PROJECTS;
                       }
                   }
                }
                else
                {
                   Mysql_dapper db = DB_MYSQL;
                        if (PROJECTS.projects_id == 0)
                       {
                            long id = 0;
                            id = db.Insert<projects>(PROJECTS);
                           if (id != 0)
                             result = db.Get<projects>(id);
                       }
                       else
                       {
                         bool ok = db.Update<projects>(PROJECTS);
                           if (ok == true)
                             result = db.Get<projects>(PROJECTS.projects_id);
                           else
                             result = PROJECTS;
                       }
                }
            }
            AfterSaveprojects(PROJECTS: PROJECTS, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteprojects(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL = null, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteprojects(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                projects etmp = Getprojects(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.projects_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.projects_active = false;
                etmp.deletedprojects_id = true;
                projects eresulttmp = Saveprojects(PROJECTS:etmp, ALLOFUSERS:ALLOFUSERS, DB_MYSQL:DB_MYSQL, SYNC:SYNC,TRAN:TRAN);
                if (eresulttmp.deletedprojects_id == true)
                    result = true;
            }
            AfterDeleteprojects(ID, ALLOFUSERS,  DB_MYSQL:DB_MYSQL, SYNC, TRAN);
            return result;
        }
        public projects Getprojects(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            projects result = new projects();
            string connstr = GetConnStr(ALLOFUSERS);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: connstr, usetransaction: false))
                {                    result = db.Get<projects>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.projects_use == false) || (result.deletedprojects_id == true) || (result.projects_active==false))
                            result = new projects();
                }
            }
            return result;
        }
        public List<projects> GetAllprojects(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<projects> result = new List<projects>();
            string connstr = GetConnStr(ALLOFUSERS);
            BeforeGetAllprojects(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_projects info = new info_projects();
                whereclause += " AND " + info.projects_deletedprojects_id + " = false AND " + info.projects_projects_use + " = true AND " + info.projects_projects_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr, usetransaction: false))
                {
                    result = db.GetAll<projects>(whereclause: whereclause).ToList();
                }            }            AfterGetAllprojects(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveprojects(projects PROJECTS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterSaveprojects(projects PROJECTS, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void AfterDeleteprojects (long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeDeleteprojects(long ID, allofusers ALLOFUSERS, Mysql_dapper DB_MYSQL, bool SYNC, bool TRAN) { }
        public void BeforeGetprojects(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetprojects(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllprojects(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllprojects(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     projects ent = JsonConvert.DeserializeObject<projects>(request.data);
                     projects save_ent = Saveprojects(ent, e_aou, DB_MYSQL, false, false);
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
                 projects ent = JsonConvert.DeserializeObject<projects>(request.data);
                 bool resu = Deleteprojects (ID: ent.projects_id, ALLOFUSERS: e_aou, DB_MYSQL:DB_MYSQL ,SYNC: false, TRAN: false);
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
                 projects ent = Getprojects(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.projects_id==ID)
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
                 List<projects> ent = GetAllprojects(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
