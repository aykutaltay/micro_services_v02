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
    public partial class Op_useractivation
    {
        public useractivation Saveuseractivation(useractivation USERACTIVATION, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            useractivation result = new useractivation();
            BeforeSaveuseractivation(USERACTIVATION: USERACTIVATION, ALLOFUSERS, SYNC:SYNC, TRAN: TRAN);
            //eğer birden fazla DataBase güncelleme var ise
            if (SYNC == true)
                USERACTIVATION.useractivation_use = false;
            //birden fazla tabloda güncelleme var ise
            if (TRAN == true)
                USERACTIVATION.useractivation_active = false;
            if ( ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    if (USERACTIVATION.useractivation_id == 0)
                    {
                        long id = 0;
                        id = db.Insert<useractivation>(USERACTIVATION);
                        if (id != 0)
                            result = db.Get<useractivation>(id);
                    }
                    else
                    {
                        bool ok = db.Update<useractivation>(USERACTIVATION);
                        if (ok == true)
                            result = db.Get<useractivation>(USERACTIVATION.useractivation_id);
                        else
                            result = USERACTIVATION;
                    }
                }
            }
            AfterSaveuseractivation(USERACTIVATION: USERACTIVATION, ALLOFUSERS, SYNC: SYNC, TRAN: TRAN);
            return result;
        }
        public bool Deleteuseractivation(long ID, allofusers ALLOFUSERS, bool SYNC = false, bool TRAN = false)
        {
            bool result = false;
            BeforeDeleteuseractivation(ID, ALLOFUSERS, SYNC, TRAN);
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                useractivation etmp = Getuseractivation(ID, ALLOFUSERS);
                //eğer birden fazla Database etkileniyor ise
                if (SYNC == true)
                    etmp.useractivation_use = false;
                //eğer birden fazla tablo etkileniyor ise
                if (TRAN == true)
                    etmp.useractivation_active = false;
                etmp.deleteduseractivation_id = true;
                useractivation eresulttmp = Saveuseractivation(etmp, ALLOFUSERS);
                if (eresulttmp.deleteduseractivation_id == true)
                    result = true;
            }
            AfterDeleteuseractivation(ID, ALLOFUSERS, SYNC, TRAN);
            return result;
        }
        public useractivation Getuseractivation(long ID, allofusers ALLOFUSERS, bool ALL=false)
        {
            useractivation result = new useractivation();
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr: ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {                    result = db.Get<useractivation>(id: ID);
                    //senkron dişinda ve silinenlerin dişindakileri getirmesi
                    if (ALL==false)
                        if ((result.useractivation_use == false) || (result.deleteduseractivation_id == true) || (result.useractivation_active==false))
                            result = new useractivation();
                }
            }
            return result;
        }
        public List<useractivation> GetAlluseractivation(string whereclause , allofusers ALLOFUSERS, bool ALL=false)
        {
            List<useractivation> result = new List<useractivation>();
            BeforeGetAlluseractivation(whereclause, ALLOFUSERS, ALL);
            //senkron dişinda ve silinenlerin dişindakileri getirmesi
            if (ALL == false)
            {
                info_useractivation info = new info_useractivation();
                whereclause += " AND " + info.useractivation_deleteduseractivation_id + " = false AND " + info.useractivation_useractivation_use + " = true AND " + info.useractivation_useractivation_active + " = true";
            }
            if (ALLOFUSERS.appdatabase_type == AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(ALLOFUSERS.appdatabase_connstr, usetransaction: false))
                {
                    result = db.GetAll<useractivation>(whereclause: whereclause).ToList();
                }            }            AfterGetAlluseractivation(whereclause, ALLOFUSERS, ALL);
            return result;
        }
        public void BeforeSaveuseractivation(useractivation USERACTIVATION, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterSaveuseractivation(useractivation USERACTIVATION, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void AfterDeleteuseractivation (long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeDeleteuseractivation(long ID, allofusers ALLOFUSERS, bool SYNC, bool TRAN) { }
        public void BeforeGetuseractivation(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void AfterGetuseractivation(long ID, allofusers ALLOFUSERS, bool ALL) { }
        public void BeforeGetAlluseractivation(string whereclause , allofusers ALLOFUSERS, bool ALL ) { }
        public void AfterGetAlluseractivation(string whereclause, allofusers ALLOFUSERS, bool ALL) { }
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
                     useractivation ent = JsonConvert.DeserializeObject<useractivation>(request.data);
                     useractivation save_ent = Saveuseractivation(ent, e_aou, false, false);
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
                 useractivation ent = JsonConvert.DeserializeObject<useractivation>(request.data);
                 bool resu = Deleteuseractivation (ID: ent.useractivation_id, ALLOFUSERS: e_aou, SYNC: false, TRAN: false);
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
                 useractivation ent = Getuseractivation(ID: ID,ALLOFUSERS:e_aou, ALL:false);
                 if (ent!=null)
                 {
                     if (ent.useractivation_id==ID)
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
                 List<useractivation> ent = GetAlluseractivation(whereclause:request.data, ALLOFUSERS: e_aou, ALL: false);
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
