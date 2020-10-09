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
    public partial class Op_appdatabase
    {
        public appdatabase Saveappdatabase(appdatabase APPDATABASE, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            appdatabase result = new appdatabase();
            BeforeSaveappdatabase(APPDATABASE: APPDATABASE, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                APPDATABASE.appdatabase_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                APPDATABASE.appdatabase_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (APPDATABASE.appdatabase_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<appdatabase>(APPDATABASE);
                        if (id != 0)
                            result = db.Get<appdatabase>(id);
                    }
                    else
                    {
                        bool ok = db.Update<appdatabase>(APPDATABASE);
                        if (ok == true)
                            result = db.Get<appdatabase>(APPDATABASE.appdatabase_id);
                        else
                            result = APPDATABASE;
                    }
                }
            }
            AfterSaveappdatabase(APPDATABASE: APPDATABASE, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteappdatabase(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteappdatabase(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                appdatabase etmp = Getappdatabase(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.appdatabase_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.appdatabase_active = false;
                etmp.deletedappdatabase_id = true;
                appdatabase eresulttmp = Saveappdatabase(etmp, ALLOFUSERS);
                if (eresulttmp.deletedappdatabase_id == true)
                    result = true;
            }
            AfterDeleteappdatabase(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public appdatabase Getappdatabase(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            appdatabase result = new appdatabase();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<appdatabase>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.appdatabase_use == false) || (result.deletedappdatabase_id == true) || (result.appdatabase_active==false))
                            result = new appdatabase();
                }
            }
            return result;
        }
        public List<appdatabase> GetAllappdatabase(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<appdatabase> result = new List<appdatabase>();
            BeforeGetAllappdatabase(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_appdatabase info = new info_appdatabase();
                whereclause += " AND " + info.appdatabase_deletedappdatabase_id + " = false AND " + info.appdatabase_appdatabase_use + " = true AND " + info.appdatabase_appdatabase_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<appdatabase>(whereclause: whereclause).ToList();
                }            }            AfterGetAllappdatabase(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveappdatabase(appdatabase APPDATABASE, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaveappdatabase(appdatabase APPDATABASE, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleteappdatabase (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleteappdatabase(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetappdatabase(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetappdatabase(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAllappdatabase(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAllappdatabase(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     appdatabase ent = JsonConvert.DeserializeObject<appdatabase>(request.data);
                     appdatabase save_ent = Saveappdatabase(ent, e_aou, false, false);
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
                 appdatabase ent = JsonConvert.DeserializeObject<appdatabase>(request.data);
                 bool resu = Deleteappdatabase (ID: ent.appdatabase_id, ALLOFUSERS: e_aou, SYNC: false, TRAN: false);
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
                 appdatabase ent = Getappdatabase(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.appdatabase_id==ID)
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
                 List<appdatabase> ent = GetAllappdatabase(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
