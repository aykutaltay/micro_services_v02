using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services.A00;
using micro_services_bus;
using micro_services_bus.zoradamlar_com_db_mic_user;
using micro_services_dal;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using micro_services_share;
using micro_services_share.Model;
using micro_services_share.vModel;
using Newtonsoft.Json;

namespace micro_services.A00_Core
{
    public class SOperation
    {

        Op_users optUsers = new Op_users();
        Op_core Op_Core = new Op_core();
        info_users infUsers = new info_users();
        allofusers default_ALLOFUSER = new allofusers()
        {
            appdatabase_connstr = AppStaticStr.core_dbConnStr,
            appdatabase_type = AppStaticStr.core_dbTypeMYSQL
        };
        Op_usersofprojects Op_uop = new Op_usersofprojects();
        info_usersofprojects infUop = new info_usersofprojects();
        public cResponse CompanyUserList(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };

            //tokenden users bilgilerini al
            allofusers e_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token).FirstOrDefault();
            if (e_aou == null)
                return result;
            //eğer admin değil ise boş dönüş yapılır
            if (e_aou.role_intvalue > 100)
                return result;

            //usersın ait olduğu companyid denbu company e ait olan herkesi al                
            List<users> l_users = optUsers.GetAllusers(string.Format("{0}={1}", infUsers.users_users_company_id, e_aou.users_company_id), ALLOFUSERS: default_ALLOFUSER);
            if (l_users == null)
                return result;

            List<mainuserlist> l_mainusrlist = new List<mainuserlist>();


            for (int i = 0; i < l_users.Count; i++)
            {
                List<allofusers> l_tmp = Op_Core.l_allofusers(l_users[i].users_id, AppStaticStr.core_dbTypeMYSQL, AppStaticStr.core_dbConnStr);

                mainuserlist mainusrlist = new mainuserlist()
                {
                    Id = l_users[i].users_id,
                    Durum = l_tmp[0].users_active == true ? "Aktif" : "Pasif",
                    Yetki = l_tmp[0].role_name == "admin" ? "Yönetici" : "Kullanıcı",
                    Kod = l_tmp[0].users_name,
                    Email = l_tmp[0].users_mail,
                    SonTarih = l_tmp[0].users_expiretime.ToString()
                };
                l_mainusrlist.Add(mainusrlist);
            }
            result.data = JsonConvert.SerializeObject(l_mainusrlist);
            result.message = AppStaticStr.msg0045OK;
            result.message_code = AppStaticInt.msg001Succes;


            return result;
        }
        public cResponse UserGet(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };
            int reqUserID = 0;
            int.TryParse(request.data, out reqUserID);

            if (reqUserID == 0)
                return result;

            List<allofusers> l_tmp = Op_Core.l_allofusers(reqUserID, AppStaticStr.core_dbTypeMYSQL, AppStaticStr.core_dbConnStr);

            if (l_tmp.Count != 1)
                return result;

            mainuserinfo e_usr = new mainuserinfo()
            {
                authValue = l_tmp[0].role_name,
                changeTime = l_tmp[0].users_updatetime.ToString(),
                createTime = l_tmp[0].users_createtime.ToString(),
                expireTime = l_tmp[0].users_expiretime.ToString(),
                langValue = l_tmp[0].lang_id,
                statuValue = Convert.ToInt32(l_tmp[0].users_active),
                userBackupMail = l_tmp[0].users_backupmail,
                userID = reqUserID,
                userMail = l_tmp[0].users_mail,
                userName = l_tmp[0].users_name
            };

            List<usersofprojects> l_uop = Op_uop.GetAllusersofprojects(whereclause: string.Format("{0}={1}", infUop.usersofprojects_usersofprojects_users_id, reqUserID)
            , ALLOFUSERS: default_ALLOFUSER);


            for (int i = 0; i < l_uop.Count; i++)
            {
                if (l_uop[i].usersofprojects_projects_id == 1)
                    e_usr.Core_project = true;
                if (l_uop[i].usersofprojects_projects_id == 2)
                    e_usr.Fason_project = true;
            }


            result.data = JsonConvert.SerializeObject(e_usr);
            result.message = AppStaticStr.msg0045OK;
            result.message_code = AppStaticInt.msg001Succes;

            return result;
        }

        public cResponse UserGetALL(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };

            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token).ToList();

            if (l_aou.Count != 1)
                return result;
            allofusers e_aou = l_aou[0];

            result.data = JsonConvert.SerializeObject(e_aou);
            result.message = AppStaticStr.msg0045OK;
            result.message_code = AppStaticInt.msg001Succes;

            return result;
        }

    }
}