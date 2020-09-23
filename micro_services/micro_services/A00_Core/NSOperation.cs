using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services_dal;
using micro_services_share;
using Newtonsoft.Json;

namespace micro_services.A00
{
    public class NSOperation
    {
        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users info_users = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users();
        micro_services_bus.zoradamlar_com_db_mic_user.Op_users bus_users = new micro_services_bus.zoradamlar_com_db_mic_user.Op_users();

        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_passofusers info_passof = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_passofusers();
        micro_services_bus.zoradamlar_com_db_mic_user.Op_passofusers bus_passof = new micro_services_bus.zoradamlar_com_db_mic_user.Op_passofusers();

        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_parameters info_parameters = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_parameters();
        micro_services_bus.zoradamlar_com_db_mic_user.Op_parameters bus_parameters = new micro_services_bus.zoradamlar_com_db_mic_user.Op_parameters();

        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_appserver info_appserver = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_appserver();
        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_appdatabase info_appdatabase = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_appdatabase();

        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_dbserver info_dbserver =new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_dbserver();
        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_company info_company = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_company();
        micro_services_bus.zoradamlar_com_db_mic_user.Op_company bus_Compny = new micro_services_bus.zoradamlar_com_db_mic_user.Op_company();
        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_useractivation info_useractivation = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_useractivation();
        micro_services_bus.zoradamlar_com_db_mic_user.Op_useractivation bus_UserActiva = new micro_services_bus.zoradamlar_com_db_mic_user.Op_useractivation();
        micro_services_dal.Models.zoradamlar_com_db_mic_user.info_usersofprojects info_usersofprojects = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_usersofprojects();
        micro_services_bus.zoradamlar_com_db_mic_user.Op_usersofprojects bus_UserOproje = new micro_services_bus.zoradamlar_com_db_mic_user.Op_usersofprojects();
        micro_services_share.vModel.allofusers default_ALLOFUSER = new micro_services_share.vModel.allofusers()
                {
                    appdatabase_connstr = AppStaticStr.core_dbConnStr,
                    appdatabase_type = AppStaticStr.core_dbTypeMYSQL
                };
        public long UserAuth(cRequest model)
        {
            //cResponse result = new cResponse()
            //{
            //    message_code = AppStaticInt.msg0001WrongUserNamePass_i,
            //    message = AppStaticStr.msg0001WrongUserNamePass,
            //    token = string.Empty,
            //    data = string.Empty
            //};
            long result = 0;
            //işlemler için manueldolduruluyor
            micro_services_share.vModel.allofusers ALLOFUSER = new micro_services_share.vModel.allofusers()
            {
                appdatabase_connstr = AppStaticStr.core_dbConnStr,
                appdatabase_type = AppStaticStr.core_dbTypeMYSQL
            };

            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("USERNAME", "Ulaş");
            test.Add("PASS", "ORUÇ");
            string ttstr = JsonConvert.SerializeObject(test);

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



            string sql = string.Format("{0}='{1}'"
                , info_users.users_users_mail, gelen[AppStaticStr.Key_username]);

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.users> l_usr = bus_users.GetAllusers(whereclause: sql
                , ALLOFUSERS: ALLOFUSER);

            if (l_usr.Count != 1)
                return result;

            sql = string.Format(" {0}={1} And {2}='{3}'"
                , info_passof.passofusers_passofusers_users_id, l_usr[0].users_id
                , info_passof.passofusers_passofusers_pass, gelen[AppStaticStr.Key_password]);

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.passofusers> l_passof = bus_passof.GetAllpassofusers(whereclause: sql
                , ALLOFUSERS: ALLOFUSER);



            if (l_passof.Count != 1)
                return result;

            //TODO: burada tarih doldu ise yine 0 gelecektir, yeni şifre verme ekranına aktarımlası gerekiyor.
            if (DateTime.Now > l_passof[0].passofusers_expiretime)
                return result;

            result = l_usr[0].users_id;


            return result;
        }
        public long NewUser(cRequest model)
        {
            //cResponse result = new cResponse()
            //{
            //    message_code = AppStaticInt.msg0001WrongUserNamePass_i,
            //    message = AppStaticStr.msg0001WrongUserNamePass,
            //    token = string.Empty,
            //    data = string.Empty
            //};
            long result = 0;

            Dictionary<string, string> gelen = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.data);

            //kontroller
            if (gelen.Count != 3)
                return result;
            if (gelen.Keys.ElementAt(0).ToString() != AppStaticStr.Key_username)
                return result;
            if (gelen.Keys.ElementAt(1).ToString() != AppStaticStr.Key_password)
                return result;
            if (gelen.Keys.ElementAt(2).ToString() != AppStaticStr.Key_namesurname)
                return result;
            if (AppStaticMethod.Cont_Injektion(gelen[AppStaticStr.Key_username]) == false)
                return result;
            if (AppStaticMethod.Cont_Injektion(gelen[AppStaticStr.Key_password]) == false)
                return result;
            if (AppStaticMethod.Cont_Injektion(gelen[AppStaticStr.Key_username]) == false)
                return result;



            string sql = string.Format("{0}='{1}'"
                , info_users.users_users_mail, gelen[AppStaticStr.Key_username]);

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.users> l_usr = bus_users.GetAllusers(whereclause: sql
                , ALLOFUSERS: default_ALLOFUSER, ALL:true);

            if (l_usr.Count == 1) //aynu kullanıcıdan var ise bu hata verilir.
                return -1;

            //eğer kullancı kodundan yak ise, sırası ile, users, pass, activation kayıtları yapılır
            //user active = false olarak kayıtlarda olur.
            //parametrelerde
            //default dbserver bilgisi
            //default app server bilgisi
            //default expire time süresi gün olarak
            DateTime tarih = DateTime.Now;

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.parameters> l_prmters = bus_parameters.GetAllparameters(
                whereclause: string.Format("{0}='{1}'"
                , info_parameters.parameters_parameters_name, AppStaticStr.str_DefaultValuesForNewUser)
                , ALLOFUSERS: default_ALLOFUSER
            );

            int db_id = 0;
            int app_id = 0;
            int exp_day =0;

            for (int i = 0; i < l_prmters.Count; i++)
            {
                if (l_prmters[i].parameters_valuestring.ToString() == info_appserver.appserver_appserver_id.ToString())
                    app_id = l_prmters[i].parameters_valueint;
                if (l_prmters[i].parameters_valuestring == info_dbserver.dbserver_dbserver_id)
                    db_id = l_prmters[i].parameters_valueint;
                if (l_prmters[i].parameters_valuestring==AppStaticStr.str_UserExpireDayDefault)
                    exp_day=l_prmters[i].parameters_valueint;
            }
            //Entity lerin hazırlanması
            #region Entity lerin hazırlanması
            micro_services_dal.Models.zoradamlar_com_db_mic_user.company e_comp = new micro_services_dal.Models.zoradamlar_com_db_mic_user.company()
            {
                company_appserver_id = app_id,
                company_dbserver_id = db_id,
                company_expiretime = tarih.AddDays(exp_day),
                company_active = true,
                company_createtime = tarih,
                company_name = gelen[AppStaticStr.Key_namesurname],
                company_updatetime = tarih,
                company_use = true,
                deletedcompany_id = false,
                company_id = 0
            };

            micro_services_dal.Models.zoradamlar_com_db_mic_user.users e_users = new micro_services_dal.Models.zoradamlar_com_db_mic_user.users()
            {
                users_company_id=0,
                users_id=0,
                users_mail=gelen[AppStaticStr.Key_username],
                users_name=gelen[AppStaticStr.Key_username],
                users_backupmail = "",
                users_createtime=tarih,
                users_expiretime=tarih,
                users_updatetime=tarih,
                users_lang_id=1, 
                users_role_id=2, //ilk açılan kullanıcı herzaman admin olacak
                deletedusers_id = false,
                users_active = false,
                users_use=true
            };

            micro_services_dal.Models.zoradamlar_com_db_mic_user.passofusers e_pass = new micro_services_dal.Models.zoradamlar_com_db_mic_user.passofusers()
            {
                passofusers_users_id = 0,
                passofusers_id = 0,
                passofusers_createtime =tarih,
                passofusers_expiretime=tarih.AddDays(exp_day-30),
                passofusers_active=true,
                deletedpassofusers_id=false,
                //passofusers_pass=AppStaticMethod.strEncrypt(gelen[AppStaticStr.Key_password]),
                passofusers_pass=gelen[AppStaticStr.Key_password],
                passofusers_use=true
            };

            micro_services_dal.Models.zoradamlar_com_db_mic_user.useractivation e_usract = new micro_services_dal.Models.zoradamlar_com_db_mic_user.useractivation()
            {
                useractivation_code = Guid.NewGuid().ToString(),
                useractivation_id = 0,
                useractivation_user_id = 0,
                useractivation_createtime = tarih,
                deleteduseractivation_id = true,
                useractivation_active = true,
                useractivation_use = true
            };

            micro_services_dal.Models.zoradamlar_com_db_mic_user.usersofprojects e_usrproject = new micro_services_dal.Models.zoradamlar_com_db_mic_user.usersofprojects()
            {
                usersofprojects_id = 0,
                usersofprojects_users_id=0,
                usersofprojects_projects_id =1, //default core proje id'si 
                deletedusersofprojects_id=false,
                usersofprojects_active=true,
                usersofprojects_use=true
            };
            #endregion Entity lerin hazırlanması

            string strtables = string.Empty;
            string strvalues = string.Empty;
            //entitiy üzerinden kayıtlar, deleted ile yapılıyor, sonrasında tek bir update var, orada hata verirse bile kayıtlar silinmiş olarak veri tabanında olacak
            #region Entitiy ile çoklu kayda uygun kayıt (deleted=true olarak)
            e_comp.deletedcompany_id = true;
            micro_services_dal.Models.zoradamlar_com_db_mic_user.company s_comp =  bus_Compny.Savecompany(COMPANY: e_comp, ALLOFUSERS: default_ALLOFUSER);
            if (s_comp.company_id == 0)
                return result;
            strtables += info_company.company_tablename;
            strvalues += s_comp.company_id.ToString();

            e_users.deletedusers_id = true;
            e_users.users_company_id = s_comp.company_id;
            micro_services_dal.Models.zoradamlar_com_db_mic_user.users s_users = bus_users.Saveusers(USERS: e_users, ALLOFUSERS: default_ALLOFUSER);
            if (s_users.users_id == 0)
                return result;
            strtables += "," + info_users.users_tablename;
            strvalues += "," + s_users.users_id;

            e_pass.deletedpassofusers_id = true;
            e_pass.passofusers_users_id = s_users.users_id;
            micro_services_dal.Models.zoradamlar_com_db_mic_user.passofusers s_pass = bus_passof.Savepassofusers(PASSOFUSERS: e_pass, ALLOFUSERS: default_ALLOFUSER);
            if (s_pass.passofusers_id == 0)
                return result;
            strtables += "," + info_passof.passofusers_tablename;
            strvalues += "," + s_pass.passofusers_id;

            e_usract.deleteduseractivation_id = true;
            e_usract.useractivation_user_id= s_pass.passofusers_users_id;
            micro_services_dal.Models.zoradamlar_com_db_mic_user.useractivation s_usract = bus_UserActiva.Saveuseractivation(USERACTIVATION: e_usract, ALLOFUSERS: default_ALLOFUSER);
            if (s_usract.useractivation_id == 0)
                return result;
            strtables += "," + info_useractivation.useractivation_tablename;
            strvalues += "," + s_usract.useractivation_id;

            e_usrproject.deletedusersofprojects_id = true;
            e_usrproject.usersofprojects_users_id = s_usract.useractivation_user_id;
            micro_services_dal.Models.zoradamlar_com_db_mic_user.usersofprojects s_usrproject = bus_UserOproje.Saveusersofprojects(USERSOFPROJECTS: e_usrproject, ALLOFUSERS: default_ALLOFUSER);
            if (s_usrproject.usersofprojects_id == 0)
                return result;
            strtables += "," + info_usersofprojects.usersofprojects_tablename;
            strvalues += "," + s_usrproject.usersofprojects_id;
            #endregion Entitiy ile çoklu kayda uygun kayıt (deleted=true olarak)

            //deleted lar true olarak kaydedildi ise onların tek severde update edilmesi
            if (default_ALLOFUSER.appdatabase_type==AppStaticStr.core_dbTypeMYSQL)
            {
                using (Mysql_dapper db = new Mysql_dapper(connstr:default_ALLOFUSER.appdatabase_connstr))
                {
                    if (db.MultiTableRecordActive(tables: strtables, values: strvalues) == false)
                        return result;
                }
            }

            //oluşturulan activasyon kodunun mail olarak atıması
            AppStaticMethod.ActivateMailSend(usermail: s_users.users_mail, namesurname: s_users.users_name, activekey: s_usract.useractivation_code);

            result = s_users.users_id;

            return result;
        }

        public long UserIdofActivation (string actkey)
        {
            long result=0;
            string whrclause = string.Format("{0}='{1}' and {2}=true and {3}=false and {4}=true"
            ,info_useractivation.useractivation_useractivation_code,actkey
            ,info_useractivation.useractivation_useractivation_active
            ,info_useractivation.useractivation_deleteduseractivation_id
            ,info_useractivation.useractivation_useractivation_use);

            List<micro_services_dal.Models.zoradamlar_com_db_mic_user.useractivation> l_act = bus_UserActiva.GetAlluseractivation(whereclause:whrclause
            ,ALLOFUSERS: default_ALLOFUSER, ALL:true);

            if (l_act.Count!=1)
                return result;

            micro_services_dal.Models.zoradamlar_com_db_mic_user.useractivation e_usractive = l_act[0];
            e_usractive.useractivation_active=true;

            micro_services_dal.Models.zoradamlar_com_db_mic_user.users e_users = bus_users.Getusers(
                ID:e_usractive.useractivation_user_id
                ,ALLOFUSERS:default_ALLOFUSER
                ,ALL:true);
            e_users.users_active=true;

            bus_UserActiva.Saveuseractivation(USERACTIVATION:e_usractive,ALLOFUSERS:default_ALLOFUSER);
            bus_users.Saveusers(USERS:e_users,ALLOFUSERS:default_ALLOFUSER);

            result = e_users.users_id;

            return result;
        }
    }
}
