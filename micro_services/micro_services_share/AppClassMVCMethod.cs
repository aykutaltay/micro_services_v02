using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using micro_services_share.Model;
using micro_services_share.vModel;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
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

            users e_tmp_eusers = JsonConvert.DeserializeObject<users>(newModel.data);
            mainuserinfo e_tmp_mui = JsonConvert.DeserializeObject<mainuserinfo>(model.data);

            if (e_tmp_eusers.users_id == 0) //yeni kullancı
            {
                passofusers e_tmp_assofusrs = new passofusers()
                {
                    deletedpassofusers_id = false,
                    passofusers_active = true,
                    passofusers_createtime = e_tmp_eusers.users_createtime,
                    passofusers_expiretime = e_tmp_eusers.users_expiretime.AddDays(-30),
                    passofusers_pass = e_tmp_mui.pass,
                    passofusers_use = true,
                    passofusers_id = 0,
                    passofusers_users_id = 0
                };

                useractivation e_tmp_activat = new useractivation()
                {
                    deleteduseractivation_id = false,
                    useractivation_active = true,
                    useractivation_createtime = e_tmp_eusers.users_createtime,
                    useractivation_use = true,
                    useractivation_code = Guid.NewGuid().ToString(),
                    useractivation_id = 0,
                    useractivation_users_id = 0
                };

                List<cRequest> l_req = new List<cRequest>();


                cRequest req_usr = new cRequest()
                {
                    project_code = AppStaticInt.ProjectCodeCore,
                    token = model.token,
                    data = JsonConvert.SerializeObject(e_tmp_eusers),
                    data_ex = new List<ex_data>
                    {
                        new ex_data() {id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SingleCrudSave },
                        new ex_data() {id=1,info=AppStaticStr.SrvTable,value=new info_users().users_tablename},
                        new ex_data() {id=2,info=AppStaticStr.SrvTransCrud,value=AppStaticStr.SrvSingleCrud},
                        new ex_data() {id=3,info=AppStaticStr.SrvTablePrimaryKey,value=new info_users().users_users_id},
                        new ex_data() {id=4,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudSave}
                    }
                };

                cRequest req_activ = new cRequest()
                {
                    project_code = 1,
                    token = model.token,
                    data = JsonConvert.SerializeObject(e_tmp_activat),
                    data_ex = new List<ex_data>
                    {
                        new ex_data() {id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SingleCrudSave },
                        new ex_data() {id=1,info=AppStaticStr.SrvTable,value=new info_useractivation().useractivation_tablename},
                        new ex_data() {id=2,info=AppStaticStr.SrvTransCrud,value=AppStaticStr.SrvSingleCrud},
                        new ex_data() {id=3,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudSave}
                    }

                };

                l_req.Add(req_usr);
                l_req.Add(req_activ);
                #region Gönderilecek liste eklemek için sırası önemli bu nedenle buraya eklendi
                cRequest e_tmp_getprojectlist = new cRequest()
                {
                    token = model.token,
                    data = "1=1",
                    project_code = AppStaticInt.ProjectCodeCore,
                    data_ex = new List<ex_data>
                     {
                        new ex_data(){id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SrvSingleCrud },
                        new ex_data(){id=1,info=AppStaticStr.SrvTable,value=new info_projects().projects_tablename},
                        new ex_data(){id=2,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudGetAll}
                    }
                };
                cResponse r_tmp_getprojelist = post(AppStaticStr.urlRestRefCrud, e_tmp_getprojectlist);
                List<projects> l_proj = JsonConvert.DeserializeObject<List<projects>>(r_tmp_getprojelist.data);
                for (int i = 0; i < l_proj.Count; i++)
                {
                    usersofprojects e_uop = new usersofprojects()
                    {
                        deletedusersofprojects_id = false,
                        usersofprojects_active = true,
                        usersofprojects_use = true,
                        usersofprojects_projects_id = l_proj[i].projects_id,
                        usersofprojects_id = 0,
                        usersofprojects_users_id = 0
                    };

                    l_req.Add(
                        new cRequest()
                        {
                            project_code = AppStaticInt.ProjectCodeCore,
                            token = model.token,
                            data = JsonConvert.SerializeObject(e_uop),
                            data_ex = new List<ex_data>
                                {
                                new ex_data() {id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SingleCrudSave },
                                new ex_data() {id=1,info=AppStaticStr.SrvTable,value=new info_usersofprojects().usersofprojects_tablename },
                                new ex_data() {id=2,info=AppStaticStr.SrvTransCrud,value=AppStaticStr.SrvSingleCrud},
                                new ex_data() {id=3,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudSave}
                                }
                        });
                }
                #endregion Gönderilecek liste eklemek için sırası önemli bu nedenle buraya eklendi

                cRequest req = new cRequest()
                {
                    project_code = AppStaticInt.ProjectCodeCore,
                    token = model.token,
                    data = JsonConvert.SerializeObject(l_req)
                };

                response = post(AppStaticStr.urlRestRefCrudTrans, model: req);

            }
            else //var olan kullanıcı
            {

                response = post(AppStaticStr.urlRestRefCrud, model: newModel);
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

            //-------------------------------------------------------------------------------------
            //Kaydeden kullanıcı bilgilerinin alınması
            cRequest mdl_usr = new cRequest()
            {
                data = model.token,
                token = model.token,
                project_code = AppStaticInt.ProjectCodeCore
            };
            cResponse res_user = post(AppStaticStr.urlRestUserinfo, model: mdl_usr);
            if (res_user.message_code == AppStaticInt.msg001Fail) //hata döndü ise
            {
                response.project_code = AppStaticInt.msg001Fail;
                response.data = AppStaticStr.msg0050UuserofSaved;
                return response;
            }

            allofusers e_aou = JsonConvert.DeserializeObject<allofusers>(res_user.data);
            //-------------------------------------------------------------------------------------
            //gelen bilginin alınması
            mainuserinfo e_mui = JsonConvert.DeserializeObject<mainuserinfo>(model.data);
            //-------------------------------------------------------------------------------------
            //username ve user mail kontrollerinin yapılması
            if (AppStaticMethod.Cont_Injektion(e_mui.userName) == false) //kullanıcıadında injection varsa çık
            {
                response.data = AppStaticStr.msg0070SendDataErr;
                return response;
            }
            if (AppStaticMethod.Cont_Injektion(e_mui.userMail) == false) //kullanıcıadında injection varsa çık
            {
                response.data = AppStaticStr.msg0070SendDataErr;
                return response;
            }
            if (AppStaticMethod.Cont_Injektion(e_mui.userBackupMail) == false) //kullanıcıadında injection varsa çık
            {
                response.data = AppStaticStr.msg0070SendDataErr;
                return response;
            }
            if (AppStaticMethod.EmailValidation(e_mui.userMail) == false) //mailvalidation bakılması
            {
                response.data = AppStaticStr.msg0070SendDataErr;
                return response;
            }
            if (AppStaticMethod.EmailValidation(e_mui.userBackupMail) == false) //mailvalidation bakılması
            {
                response.data = AppStaticStr.msg0070SendDataErr;
                return response;
            }
            if (e_mui.pass != e_mui.passRepat)
            {
                response.data = AppStaticStr.msg0070SendDataErr;
                return response;
            }
            //-------------------------------------------------------------------------------------
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
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudSave
                        },

                    };
                response.data = JsonConvert.SerializeObject(e_usr);
                response.project_code = AppStaticInt.msg001Succes;

                return response;

            }
            //-------------------------------------------------------------------------------------
            //eğer kullanıcı yeni ise kontrolleri ve kayıt için gönderimi
            if (e_mui.userID == 0)
            {
                //-------------------------------------------------------------------------------------
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
                    ,
                    project_code = AppStaticInt.ProjectCodeCore
                };

                cResponse k_usr = post(AppStaticStr.urlRestRefCrud, r_tmp);
                if (k_usr.message_code != AppStaticInt.msg001Fail) //eğer hata gelirse çıkar
                {

                    response.project_code = AppStaticInt.msg001Fail;
                    response.data = AppStaticStr.msg0040Hata;
                    return response;
                }

                #endregion yeni girilen kullanıcının sistemde mailinin olup olmadığının kontrolü
                //-------------------------------------------------------------------------------------
                //yeni kullanıcı için durum bilgisi mutlara pasif olmalıdır.
                e_mui.statuValue = 0;
                //-------------------------------------------------------------------------------------
                //parametre bilgilerinden expire günün alınması
                r_tmp = new cRequest()
                {
                    data = string.Format("{0}='{1}'", new info_parameters().parameters_parameters_valuestring, AppStaticStr.Prm_DefaultExpire),
                    token = model.token,
                    project_code = AppStaticInt.ProjectCodeCore,
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
                //-------------------------------------------------------------------------------------
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
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudSave
                        },
                    new ex_data(){
                        id = 3,
                        info = AppStaticStr.SrvUserPass,
                        value = e_mui.pass
                        }
                    };
                response.data = JsonConvert.SerializeObject(e_usr);
                response.project_code = AppStaticInt.msg001Succes;

                return response;

            }
            else
            //-------------------------------------------------------------------------------------
            //eğer kullanıcı var ise kontroller ve kayıt işlemi
            {
                //-------------------------------------------------------------------------------------
                //eğer kullanıcı var ise kontroller ve kayıt işlemi
                //-------------------------------------------------------------------------------------
                // Kullanıcının aktif olup olmadığının kontrolü
                cRequest r_tmp = new cRequest()
                {
                    data = string.Format("{0}={1}", new info_useractivation().useractivation_useractivation_users_id, e_mui.userID),
                    token = model.token,
                    data_ex = new List<ex_data>{
                    new ex_data{ id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SrvSingleCrud},
                    new ex_data{ id=1, info=AppStaticStr.SrvTable,value=new info_useractivation().useractivation_tablename },
                    new ex_data{id=2, info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudGetAll}
                }
                };

                cResponse k_usr = post(AppStaticStr.urlRestRefCrud, r_tmp);
                List<useractivation> l_usra = JsonConvert.DeserializeObject<List<useractivation>>(k_usr.data);
                if (l_usra == null)
                {
                    response.data = AppStaticStr.msg0080UserNotActivation;
                    return response;
                }
                if (l_usra.Count != 1)
                {
                    response.data = AppStaticStr.msg0080UserNotActivation;
                    return response;
                }
                //-------------------------------------------------------------------------------------
                //Eğer aktif ise buradan devam edecek
                r_tmp = new cRequest()
                {
                    data = string.Format("{0}={1}", new info_users().users_users_id, e_mui.userID),
                    token = model.token,
                    data_ex = new List<ex_data>{
                    new ex_data{ id=0,info=AppStaticStr.SrvSingleCrud,value=AppStaticStr.SrvSingleCrud},
                    new ex_data{ id=1, info=AppStaticStr.SrvTable,value=new info_users().users_tablename},
                    new ex_data{id=2, info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudGetAll}
                }
                };
                k_usr = post(AppStaticStr.urlRestRefCrud, r_tmp);
                List<users> l_usr = JsonConvert.DeserializeObject<List<users>>(k_usr.data);
                if (l_usr == null)
                {
                    response.data = AppStaticStr.msg0080UserNotActivation;
                    return response;
                }
                if (l_usr.Count != 1)
                {
                    response.data = AppStaticStr.msg0080UserNotActivation;
                    return response;
                }

                users e_usr = l_usr[0];
                DateTime tar = DateTime.Now;
                e_usr.users_active = e_mui.statuValue == 1 ? true : false;
                e_usr.users_backupmail = e_mui.userBackupMail;
                e_usr.users_company_id = e_aou.users_company_id;
                e_usr.users_lang_id = e_mui.langValue;
                e_usr.users_name = e_mui.userName;
                e_usr.users_role_id = e_mui.authValue == AppStaticStr.strAdmin ? 2 : 8; //burası el ile girildi
                e_usr.users_updatetime = tar;

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
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudSave
                        },
                    };

                response.data = JsonConvert.SerializeObject(e_usr);
                response.project_code = AppStaticInt.msg001Succes;
            }




            return response;
        }


    }
}
