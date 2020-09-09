using System.Collections.Generic;
using System.Linq;
using micro_services_dal;
using micro_services_share;
using micro_services_share.vModel;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;

namespace micro_services_bus.zoradamlar_com_db_mic_user
{
    public partial class Op_projects
    {
        public projects Saveprojects(projects PROJECTS, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            projects result = new projects();
            BeforeSaveprojects(PROJECTS: PROJECTS, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                PROJECTS.projects_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                PROJECTS.projects_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
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
            AfterSaveprojects(PROJECTS: PROJECTS, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteprojects(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteprojects(ID, ALLOFUSERS, SYNC, TRAN);
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
                projects eresulttmp = Saveprojects(etmp, ALLOFUSERS);
                if (eresulttmp.deletedprojects_id == true)
                    result = true;
            }
            AfterDeleteprojects(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public projects Getprojects(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            projects result = new projects();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
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
            BeforeGetAllprojects(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_projects info = new info_projects();
                whereclause += "AND " + info.projects_deletedprojects_id + " = false AND " + info.projects_projects_use + " = true AND " + info.projects_projects_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<projects>(whereclause: whereclause).ToList();
                }            }            AfterGetAllprojects(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveprojects(projects PROJECTS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaveprojects(projects PROJECTS, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleteprojects (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleteprojects(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetprojects(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetprojects(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllprojects(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllprojects(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
    }

}
