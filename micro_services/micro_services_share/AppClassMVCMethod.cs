using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using micro_services_share.Model;
using micro_services_share.vModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace micro_services_share
{
    public class AppClassMVCMethod
    {
        #region Constructure
        HttpClient client;
        public AppClassMVCMethod(string token = "")
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);

            if (token.Length > 1)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        #endregion Constructure

        public cResponse post(string urladdr, cRequest model)
        {
            cResponse response = new cResponse()
            {
                token = "",
                data = "",
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata
            };

            try
            {
                var fromBody = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var downloadTask = client.PostAsync(urladdr, fromBody);
                downloadTask.Wait();
                var downloadResult = downloadTask.Result;
                var stringTask = downloadResult.Content.ReadAsStringAsync();
                stringTask.Wait();

                response = JsonConvert.DeserializeObject<cResponse>(stringTask.Result);

            }
            catch (Exception)
            {


            }

            return response;
        }

        public cResponse SaveUser(cRequest model)
        {
            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = model.token,
                data = string.Empty
            };

            cRequest newModel = new cRequest();

            newModel = SaveUserCheck(model);
            if (newModel.project_code == AppStaticInt.msg001Fail)
            {
                response.message_code = newModel.project_code;
                response.message = newModel.data;
                return response;
            }







            return response;
        }

        public cRequest SaveUserCheck(cRequest model)
        {
            cRequest response = new cRequest()
            {
                token = model.token,
                project_code = AppStaticInt.msg001Fail,
                data = string.Empty

            };


            //Kaydeden kullanıcı bilgilerinin alınması
            cRequest mdl_usr = new cRequest()
            {
                data = model.token,
                token = model.token
            };
            cResponse res_user = post(AppStaticStr.urlRestUserinfo, model: mdl_usr);
            if (res_user.message_code == AppStaticInt.msg001Fail) //hata döndü ise
            {
                response.project_code = AppStaticInt.msg001Fail;
                response.data = AppStaticStr.msg0050UuserofSaved;
                return response;
            }

            allofusers e_aou = JsonConvert.DeserializeObject<allofusers>(res_user.data);

            //gelen bilginin alınması
            mainuserinfo e_mui = JsonConvert.DeserializeObject<mainuserinfo>(model.data);
            //username ve user mail kontrollerinin yapılması

            //eğer kullanıcı ile kaydeden aynı ise ve admin ise
            if ((e_mui.userID == e_aou.users_id) && (e_aou.role_intvalue <= AppStaticInt.RoleAdminponit))
            {
                users e_usr = new users()
                {
                    users_backupmail = e_mui.userBackupMail, //değiştirilebilecek alan
                    users_lang_id = e_mui.langValue, //değiştirilebilen alan
                    users_id = e_aou.users_id,
                    deletedusers_id = e_aou.deletedusers_id,
                    users_active = e_aou.users_active,
                    users_company_id = e_aou.users_company_id,
                    users_createtime = e_aou.users_createtime,
                    users_expiretime = e_aou.users_expiretime,
                    users_mail = e_aou.users_mail,
                    users_name = e_mui.userName,
                    users_role_id = e_aou.users_role_id,
                    users_updatetime = e_aou.users_updatetime,
                    users_use = e_aou.users_use
                };

                response.data_ex = new List<ex_data>() {
                    new ex_data(){
                        id = 0,
                        info = AppStaticStr.SrvSingleCrud,
                        value = AppStaticStr.SrvSingleCrud
                        },
                    new ex_data(){
                        id = 1,
                        info = AppStaticStr.SrvTable,
                        value = new info_users().users_tablename
                        },
                    new ex_data(){
                        id = 1,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudSave
                        },

                    };

                response.project_code = AppStaticInt.msg001Succes;
                return response;

            }

            //girilen yeni kullanıcının kontrolleri 
            if (e_mui.userID == 0)
            {
                // yeni kullanıcının mailinden sistemde olup olmadığının kontrolü
                #region yeni girilen kullanıcının sistemde mailinin olup olmadığının kontrolü
                cRequest r_tmp = new cRequest()
                {
                    data = string.Format("{0}='{1}'", new info_users().users_users_mail, e_mui.userMail),
                    token = model.token,
                    data_ex = new List<ex_data>{
                    new ex_data{ id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SrvSingleCrud},
                    new ex_data{ id=1, info=AppStaticStr.SrvTable,value=new info_users().users_tablename},
                    new ex_data{id=2, info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudGetAll}
                }
                };

                cResponse k_usr = post(AppStaticStr.urlRestRefCrud, r_tmp);
                if (k_usr.message_code == AppStaticInt.msg001Fail) //eğer hata gelirse çıkar
                {
                    response.project_code = AppStaticInt.msg001Fail;
                    response.data = AppStaticStr.msg0040Hata;
                    return response;
                }
                List<users> l_u_tmp = JsonConvert.DeserializeObject<List<users>>(k_usr.data);
                if (l_u_tmp != null) //eğer bir bilgi geldi ise yeni kullanıcı olarak girilemeyeceği için işlem iptal edilir.
                {
                    response.project_code = AppStaticInt.msg001Fail;
                    response.data = AppStaticStr.msg0060UserAllready;
                    return response;
                }
                #endregion yeni girilen kullanıcının sistemde mailinin olup olmadığının kontrolü

                //yeni kullanıcı için durum bilgisi mutlara pasif olmalıdır.
                e_mui.statuValue = 0;

                //parametre bilgilerinden expire günün alınması
                r_tmp = new cRequest()
                {
                    data = string.Format("{0}='{1}'", new info_parameters().parameters_parameters_valuestring, AppStaticStr.Prm_DefaultExpire),
                    token = model.token,
                    data_ex = new List<ex_data>{
                    new ex_data{ id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SrvSingleCrud},
                    new ex_data{ id=1, info=AppStaticStr.SrvTable,value=new info_parameters().parameters_tablename},
                    new ex_data{id=2, info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudGetAll}
                }
                };
                k_usr = post(AppStaticStr.urlRestRefCrud, r_tmp);
                List<parameters> l_p_tmp = JsonConvert.DeserializeObject<List<parameters>>(k_usr.data);
                int expday = 0;
                if (l_p_tmp != null)
                    expday = l_p_tmp[0].parameters_valueint;



                //kayıt için entity doldurulması
                DateTime tar = DateTime.Now;
                users e_usr = new users
                {
                    deletedusers_id = false,
                    users_active = false,
                    users_backupmail = e_mui.userBackupMail,
                    users_company_id = e_aou.users_company_id,
                    users_createtime = tar,
                    users_expiretime = tar.AddDays(expday),
                    users_id = 0,
                    users_lang_id = e_mui.langValue,
                    users_mail = e_mui.userMail,
                    users_name = e_mui.userName,
                    users_role_id = e_mui.authValue == AppStaticStr.strAdmin ? 2 : 8, //burası el ile girildi
                    users_updatetime = tar,
                    users_use = true
                };


            }

            return response;
        }


    }
}
