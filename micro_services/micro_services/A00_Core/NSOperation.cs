using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services_share;
using Newtonsoft.Json;

namespace micro_services.A00
{
    public class NSOperation
    {
        public cResponse UserAuth(cRequest model)
        {
            cResponse result = new cResponse()
            {
                message_code = AppStaticInt.msg0001WrongUserNamePass_i,
                message = AppStaticStr.msg0001WrongUserNamePass,
                token = string.Empty,
                data = string.Empty
            };

            Dictionary<string, string> gelen = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.data);

            //kontroller
            if (gelen.Count != 2)
                return result;
            if (gelen.Keys.ElementAt(0).ToString() != AppStaticStr.Key_username)
                return result;
            if (gelen.Keys.ElementAt(1).ToString() != AppStaticStr.Key_password)
                return result;
            if (AppStaticMethod.Cont_Injektion(gelen[AppStaticStr.Key_username]) == false)
                return result;
            if (AppStaticMethod.Cont_Injektion(gelen[AppStaticStr.Key_password]) == false)
                return result;



            micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users info_users = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users();
            micro_services_bus.zoradamlar_com_db_mic_user.Op_users bus_users = new micro_services_bus.zoradamlar_com_db_mic_user.Op_users();

            micro_services_dal.Models.zoradamlar_com_db_mic_user.info_passofusers info_passof = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_passofusers();
            micro_services_bus.zoradamlar_com_db_mic_user.Op_passofusers bus_passof = new micro_services_bus.zoradamlar_com_db_mic_user.Op_passofusers();


            string sql = string.Format("{0}='{1}'"
                , info_users.users_users_mail, gelen[AppStaticStr.Key_username]);

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.users> l_usr = bus_users.GetAllusers(whereclause: sql
                , DBTYPE: AppStaticStr.core_dbTypeMYSQL
                , CONNSTR: AppStaticStr.core_dbConnStr);

            if (l_usr.Count != 1)
                return result;

            sql = string.Format("{0}={1} And {2}='{3}' And {4}>'{5}'"
                ,info_passof.passofusers_passofusers_users_id,l_usr[0].users_id
                ,info_passof.passofusers_passofusers_pass,gelen[AppStaticStr.Key_password]
                ,info_passof.passofusers_passofusers_expiretime,DateTime.Now.ToString("yyyy-MM-dd"));

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.passofusers> l_passof = bus_passof.GetAllpassofusers(whereclause: sql
                , DBTYPE: AppStaticStr.core_dbTypeMYSQL
                , CONNSTR: AppStaticStr.core_dbConnStr);

            //TODO: burada tarih doldu ise yine 0 gelecektir, yeni şifre verme ekranına aktarımlası gerekiyor.

            if (l_passof.Count != 1)
                return result;



            return result;
        }
    }
}
