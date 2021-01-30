using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        classToken ctoken = new classToken();
        // Methodlar 
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
            allofusers e_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).FirstOrDefault();
            if (e_aou == null)
                return result;
            //eğer admin değil ise boş dönüş yapılır
            if (e_aou.role_intvalue > 100)
                return result;

            //usersın ait olduğu companyid denbu company e ait olan herkesi al                
            List<users> l_users = optUsers.GetAllusers(string.Format("{0}={1}", infUsers.users_users_company_id, e_aou.users_company_id), ALLOFUSERS: default_ALLOFUSER, ALL: true);
            if (l_users == null)
                return result;

            List<mainuserlist> l_mainusrlist = new List<mainuserlist>();


            for (int i = 0; i < l_users.Count; i++)
            {
                List<allofusers> l_tmp = Op_Core.l_allofusers(l_users[i].users_id, request.project_code, AppStaticStr.core_dbTypeMYSQL, AppStaticStr.core_dbConnStr);

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

            List<allofusers> l_tmp = Op_Core.l_allofusers(reqUserID, request.project_code, AppStaticStr.core_dbTypeMYSQL, AppStaticStr.core_dbConnStr);

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

            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).ToList();

            if (l_aou.Count != 1)
                return result;
            allofusers e_aou = l_aou[0];

            result.data = JsonConvert.SerializeObject(e_aou);
            result.message = AppStaticStr.msg0045OK;
            result.message_code = AppStaticInt.msg001Succes;

            return result;
        }
        public cResponse userRetoken(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };

            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).ToList();
            try
            {
                if (l_aou.Count != 1)
                    return result;
                allofusers e_aou = l_aou[0];
                string newtoken = ctoken.generateJwtToken(e_aou.users_id);
                long kontsavetoken = 0;
                kontsavetoken = ctoken.savetoken(e_aou.users_id, newtoken);
                if (kontsavetoken != 0)
                    ctoken.savestaticList(e_aou.users_id, request.project_code);

                result.token = newtoken;
                result.data = newtoken;
                result.message_code = AppStaticInt.msg001Succes;
                result.message = AppStaticStr.msg0045OK;
            }
            catch (System.Exception)
            {

            }

            return result;
        }
        public cResponse ref_crud(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };

            #region kullanıcı bilgilerinin alınması
            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).ToList();

            if (l_aou == null) return result;
            if (l_aou.Count != 1) return result;

            #endregion kullanıcı bilgilerinin alınması

            #region gelen paket içinden yapılacak işlemin bilgilerinin alınması
            List<ex_data> l_ed_crd = request.data_ex.Where(w => w.info == AppStaticStr.SrvSingleCrud).ToList();
            List<ex_data> l_ed_tbl = request.data_ex.Where(w => w.info == AppStaticStr.SrvTable).ToList();

            if (l_ed_crd == null) return result;
            if (l_ed_tbl == null) return result;

            if (l_ed_crd.Count != 1) return result;
            if (l_ed_tbl.Count != 1) return result;


            #endregion gelen paket içinden yapılacak işlemin bilgilerinin alınması
            //reflection yapılacak bilginin hazırlanması
            string optpath = string.Format("{0}.{1}.Op_{2}"
            , AppStaticStr.MicroServicesBus
            , l_aou[0].dbserver_name
            , l_ed_tbl[0].value
            );

            string opt = string.Format("{0}"
            , l_ed_crd[0].value);
            string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + AppStaticStr.DllMicServBus;
            System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFrom(path);
            Type myTypeOpt = ass.GetType(optpath);


            //Type Testmy =Type.GetType("micro_services.A00.NSOperation");

            //object calcInstance = Activator.CreateInstance(Type.GetType(optpath), "entiyti",allofusers,"sync", "tran");
            //object calcInstance = Activator.CreateInstance(Type.GetType(optpath), request, l_aou[0]);
            //object calcInstance = Activator.CreateInstance(Type.GetType(optpath),string.Format("Op_{0}",l_ed_tbl[0].value));
            object calcInstance = Activator.CreateInstance(myTypeOpt);

            try
            {
                string values = (string)myTypeOpt.InvokeMember(
                    //string.Format("{0}_{1}Op.", capsule.prosesName, capsule.backMessage),
                    //"Get_c_paramOp",
                    opt,
                BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                //null, calcInstance, new object[] { JsonConvert.DeserializeObject(capsule.backDataStr).ToString() });
                null, calcInstance, new object[] { request, l_aou[0], null });


                result = JsonConvert.DeserializeObject<cResponse>(values);

            }
            catch (Exception ex)
            {
                result.message = ex.ToString();
            }




            return result;
        }
        public cResponse ref_crud_tran(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };
            #region kullanıcı bilgilerinin alınması
            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).ToList();

            if (l_aou == null) return result;
            if (l_aou.Count != 1) return result;

            #endregion kullanıcı bilgilerinin alınması
            List<cRequest> l_req = JsonConvert.DeserializeObject<List<cRequest>>(request.data);
            string strPrikey = string.Empty;
            string strtmpPrikey = string.Empty;
            long lngPriKey = 0;

            using (Mysql_dapper db = new Mysql_dapper(connstr: GetConnStr(l_aou[0]), usetransaction: false))
            {
                db.BeginTransaction();

                for (int i = 0; i < l_req.Count; i++)
                {
                    #region gelen paket içinden yapılacak işlemin bilgilerinin alınması
                    List<ex_data> l_ed_crd = l_req[i].data_ex.Where(w => w.info == AppStaticStr.SrvTransCrud).ToList();
                    List<ex_data> l_ed_tbl = l_req[i].data_ex.Where(w => w.info == AppStaticStr.SrvTable).ToList();
                    List<ex_data> l_ed_pri = l_req[i].data_ex.Where(w => w.info == AppStaticStr.SrvTablePrimaryKey).ToList();


                    if (l_ed_crd == null) return result;
                    if (l_ed_tbl == null) return result;

                    if (l_ed_crd.Count != 1) return result;
                    if (l_ed_tbl.Count != 1) return result;

                    if (i == 0)
                    {
                        if (l_ed_pri != null)
                        {
                            if (l_ed_pri.Count == 1)
                            {
                                if (l_ed_pri[0].value.Length > 2)
                                    strPrikey = l_ed_pri[0].value;
                                strtmpPrikey = string.Format(@"""{0}"":", strPrikey);
                                strtmpPrikey = "\\\"" + l_ed_pri[0].value + "\\\":";
                            }
                        }
                    }

                    #endregion gelen paket içinden yapılacak işlemin bilgilerinin alınması
                    //-------------------------------------------------------------------------------
                    //reflection yapılacak bilginin hazırlanması
                    string optpath = string.Format("{0}.{1}.Op_{2}"
                    , AppStaticStr.MicroServicesBus
                    , l_aou[0].dbserver_name
                    , l_ed_tbl[0].value
                    );

                    string opt = string.Format("{0}"
                    , l_ed_crd[0].value
                    );

                    string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + AppStaticStr.DllMicServBus;
                    System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFrom(path);
                    Type myTypeOpt = ass.GetType(optpath);

                    object calcInstance = Activator.CreateInstance(myTypeOpt);
                    //-------------------------------------------------------------------------------
                    //Transaction a göre ayarlama
                    cRequest r_tmp = l_req[i];
                    if (lngPriKey != 0) //pri key değişmiş ise
                    {
                        string strFrgkey = "" + l_ed_tbl[0].value + "_" + strPrikey + "\"" + ":0";
                        string strtmpFrgkey = "" + l_ed_tbl[0].value + "_" + strPrikey + "\"" + ":" + lngPriKey.ToString();
                        r_tmp.data = r_tmp.data.Replace(strFrgkey, strtmpFrgkey);
                    }

                    try
                    {
                        string values = (string)myTypeOpt.InvokeMember(
                            opt,
                        BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                        null, calcInstance, new object[] { r_tmp, l_aou[0], db });

                        result = JsonConvert.DeserializeObject<cResponse>(values);
                        //-------------------------------------------------------------------------------
                        //Gönderilen ilk entiti ise ve primary için birşeyler yazılmış ise lng bilgiyi al

                        if (i == 0)
                        {
                            if (l_ed_pri != null)
                            {
                                if (l_ed_pri.Count == 1)
                                {
                                    lngPriKey = GetPriKeyValue(jsonValue: values, prikey: strtmpPrikey);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        db.RollBack();
                        result.message = ex.ToString();
                    }

                }

                db.Commit();
            }


            result.message = AppStaticStr.msg0045OK;
            result.message_code = AppStaticInt.msg001Succes;
            return result;
        }
        private string GetConnStr(allofusers ALLOFUSERS)

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
        private long GetPriKeyValue(string jsonValue, string prikey)
        {
            long result = 0;
            string[] divede = jsonValue.Split(prikey);
            string vval = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                if (divede[1].Substring(i, 1).ToString() != ",")
                {
                    vval = vval + divede[1].Substring(i, 1).ToString();
                }
                else
                    break;
            }

            long.TryParse(vval, out result);

            return result;
        }

        public cResponse SendActMail(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata
            };

            long usr_id = 0;
            long.TryParse(request.data, out usr_id);

            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).ToList();
            if (l_aou == null) return result;
            if (l_aou.Count != 1) return result;

            users e_usr = optUsers.Getusers(usr_id, ALLOFUSERS: l_aou[0], ALL: true);

            List<useractivation> l_act = new Op_useractivation().GetAlluseractivation(whereclause: string.Format("{0}={1}"
            , new info_useractivation().useractivation_useractivation_users_id, e_usr.users_id), ALLOFUSERS: default_ALLOFUSER);

            if (l_act == null) return result;
            if (l_act.Count != 1) return result;

            try
            {
                AppStaticMethod.ActivateMailSend(usermail: e_usr.users_mail
                , namesurname: e_usr.users_name
                , activekey: l_act[0].useractivation_code);

                result.message_code = AppStaticInt.msg001Succes;
                result.message = AppStaticStr.msg0045OK;
            }
            catch (System.Exception)
            {


            }


            return result;
        }

        public cResponse UserGet_AfterstatickListRefresh(cRequest request)
        {
            cResponse result = new cResponse()
            {
                token = request.token,
                data = "",
                message = AppStaticStr.msg0040Hata,
                message_code = AppStaticInt.msg001Fail
            };

            List<allofusers> l_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).ToList();

            if (l_aou.Count != 1)
                return result;
            allofusers e_aou = l_aou[0];

            ctoken.savestaticList(e_aou.users_id, request.project_code);


            result.data = JsonConvert.SerializeObject(e_aou);
            result.message = AppStaticStr.msg0045OK;
            result.message_code = AppStaticInt.msg001Succes;

            return result;
        }

        public cResponse flutSaveUser(cRequest request)
        {
            cResponse result = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = string.Empty,
                data = string.Empty
            };

            allofusers e_aou = AppStaticModel.l_allofusers.Where(w => w.tokensofusers_token == request.token && w.projects_id == request.project_code).FirstOrDefault();
            if (e_aou == null)
                return result;

            List<parameters> l_parameters = new Op_parameters().GetAllparameters(whereclause: string.Format("{0}='{1}'", new info_parameters().parameters_parameters_valuestring, AppStaticStr.Prm_DefaultExpire)
            , ALLOFUSERS: default_ALLOFUSER);
            if (l_parameters.Count == 0)
                return result;

            users income = JsonConvert.DeserializeObject<users>(request.data);
            users e_users = new users();
            //companyid_createtime,expiretime
            //gelen bilgi var olan bir kayıt ise
            if (income.users_id != 0)
            {
                users tmp_users = optUsers.Getusers(income.users_id, ALLOFUSERS: default_ALLOFUSER);
                e_users = income;

                e_users.deletedusers_id = tmp_users.deletedusers_id;
                e_users.users_use = tmp_users.users_use;
                e_users.users_company_id = tmp_users.users_company_id;
                e_users.users_createtime = tmp_users.users_createtime;
                e_users.users_expiretime = tmp_users.users_expiretime;

                e_users.users_mail = tmp_users.users_mail;
            }
            else
            {
                e_users = income;

                e_users.deletedusers_id = false;
                e_users.users_use = true;
                e_users.users_company_id = e_aou.users_company_id;
                e_users.users_createtime = e_users.users_updatetime;
                e_users.users_expiretime = e_users.users_createtime.AddDays(l_parameters[0].parameters_valueint);
            }
            // user bilgisi oluşturuldu

            if (e_users.users_id == 0)
            {
                //eğer yeni kullanıcı ise
                using (Mysql_dapper db = new Mysql_dapper())
                {
                    db.BeginTransaction();
                    try
                    {
                        //kullanıcı kaydı
                        users e_saveusers = optUsers.Saveusers(e_users, default_ALLOFUSER, db);
                        //---------------------------------------------------------------------------------------------
                        //aktivasyon kaydı
                        useractivation e_tmp_activat = new useractivation()
                        {
                            deleteduseractivation_id = false,
                            useractivation_active = true,
                            useractivation_createtime = e_saveusers.users_createtime,
                            useractivation_use = true,
                            useractivation_code = Guid.NewGuid().ToString(),
                            useractivation_id = 0,
                            useractivation_users_id = e_saveusers.users_id
                        };
                        new Op_useractivation().Saveuseractivation(e_tmp_activat, default_ALLOFUSER, db);
                        //---------------------------------------------------------------------------------------------
                        //projectlere dahil edilmesi
                        List<projects> l_proj = new Op_projects().GetAllprojects(whereclause: "1=1", default_ALLOFUSER);
                        for (int i = 0; i < l_proj.Count; i++)
                        {
                            usersofprojects e_uop = new usersofprojects()
                            {
                                deletedusersofprojects_id = false,
                                usersofprojects_active = true,
                                usersofprojects_id = 0,
                                usersofprojects_projects_id = l_proj[i].projects_id,
                                usersofprojects_use = true,
                                usersofprojects_users_id = e_saveusers.users_id

                            };

                            new Op_usersofprojects().Saveusersofprojects(e_uop, default_ALLOFUSER, db);
                        }
                        //---------------------------------------------------------------------------------------------

                        db.Commit();

                        //aktivasyon maili gönderimi
                        cRequest actmail = new cRequest() {
                            data=e_saveusers.users_id.ToString(),
                            token=request.token,
                            project_code=AppStaticInt.ProjectCodeCore
                        };
                        SendActMail(actmail);
                        //---------------------------------------------------------------------------------------------
                        //Sistemin oluşturduğu şifrenin gönderilmesi
                        cRequest newpassfromsystem = new cRequest() {
                            data=e_saveusers.users_mail,
                            token=request.token,
                            project_code=AppStaticInt.ProjectCodeCore
                        };

                        new NSOperation().ForgetPass(model: newpassfromsystem);
                        //---------------------------------------------------------------------------------------------
                        //Kayıt işlemi sonrasında  olanlar
                        //Kapsülü hazırla
                        result.data = JsonConvert.SerializeObject(e_saveusers);
                        result.message_code = AppStaticInt.msg001Succes;
                        result.message = AppStaticStr.msg0045OK;
                        //---------------------------------------------------------------------------------------------

                    }
                    catch (System.Exception e)
                    {

                        result.data = e.ToString();
                        result.message_code = AppStaticInt.msg001Fail;
                        result.message = AppStaticStr.msg0040Hata;

                        db.RollBack();
                    }



                }
            }
            //var olan kullanıcı ise
            else
            {
                result.data = JsonConvert.SerializeObject(optUsers.Saveusers(e_users, default_ALLOFUSER));
                result.message_code = AppStaticInt.msg001Succes;
                result.message = AppStaticStr.msg0045OK;
            }



            return result;
        }
    }
}