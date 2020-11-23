using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using micro_services_dal.Models.zoradamlar_com_db_mic_user;
using micro_services_share;
using micro_services_share.Model;
using micro_services_share.vModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using ZaMvcV03.Models;

namespace ZaMvcV03.Controllers
{
    public class OptCoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MVCNewUser([FromBody] cRequest Model)
        {
            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0001WrongUserNamePass,
                token = string.Empty,
                data = string.Empty
            };

            return Ok(response);
        }

        public IActionResult auth([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestAuth, model: Model);

            return Ok(response);
        }

        public IActionResult nuser([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestAuth, model: Model);

            return Ok(response);
        }

        public IActionResult mainuserlist([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestMainUserList, model: Model);

            return Ok(response);
        }

        public IActionResult mainuserget([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestMainUserGet, model: Model);

            return Ok(response);
        }

        public IActionResult tokenRenew([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };
            try
            {
                AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
                response = mvcPost.post(AppStaticStr.urlRestUserinfo, model: Model);

                allofusers e_aou = JsonConvert.DeserializeObject<allofusers>(response.data);
                response.message_code = AppStaticInt.msg001Fail;
                response.message = AppStaticStr.msg0040Hata;
                DateTime retokenTime = e_aou.tokensofusers_expiretime.AddMinutes(-60);

                if (retokenTime < DateTime.Now)
                {
                    response = mvcPost.post(AppStaticStr.urlRestNewToken, model: Model);
                }


            }
            catch (Exception)
            {

            }

            return Ok(response);
        }

        public IActionResult saveUser([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new AppClassMVCMethod(Model.token).SaveUser(Model);
            return Ok(response);
        }

        public IActionResult usermailActivete([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };

            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestUserActMail, model: Model);

            return Ok(response);

        }
        public IActionResult deleteUserid([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;
            Model.data_ex = new List<ex_data>() {
                    new ex_data(){
                        id = 0,
                        info = AppStaticStr.SrvSingleCrud,
                        value = AppStaticStr.SrvSingleCrud
                        },
                    new ex_data(){
                        id = 1,
                        info = AppStaticStr.SrvTable,
                        value = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users().users_tablename
                        },
                    new ex_data(){
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudGet
                        },
                    };

            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };

            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestRefCrud, model: Model);

            Model.data = response.data;
            Model.data_ex = new List<ex_data>() {
                    new ex_data(){
                        id = 0,
                        info = AppStaticStr.SrvSingleCrud,
                        value = AppStaticStr.SrvSingleCrud
                        },
                    new ex_data(){
                        id = 1,
                        info = AppStaticStr.SrvTable,
                        value = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users().users_tablename
                        },
                    new ex_data(){
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudDelete
                        },
                    };

            response = mvcPost.post(AppStaticStr.urlRestRefCrud, model: Model);

            return Ok(response);

        }

        public IActionResult getCompanyID([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;
            //Model.data_ex = new List<ex_data>() {
            //        new ex_data(){
            //            id = 0,
            //            info = AppStaticStr.SrvSingleCrud,
            //            value = AppStaticStr.SrvSingleCrud
            //            },
            //        new ex_data(){
            //            id = 1,
            //            info = AppStaticStr.SrvTable,
            //            value = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_users().users_tablename
            //            },
            //        new ex_data(){
            //            id = 2,
            //            info = AppStaticStr.SrvOpt,
            //            value = AppStaticStr.SingleCrudGet
            //            },
            //        };

            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };

            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestUserinfo, model: Model);

            response.data = JsonConvert.DeserializeObject<allofusers>(response.data).users_company_id.ToString();

            return Ok(response);
        }
        public IActionResult getCompanyName([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };

            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestUserinfo, model: Model);

            response.data = JsonConvert.DeserializeObject<allofusers>(response.data).company_name.ToString();

            return Ok(response);
        }

        public IActionResult saveCompany([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;
            string[] bolucu = Model.data.Split("*+-");


            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };

            if (bolucu.Count() != 2)
                return Ok(response);

            Model.data = bolucu[0];
            Model.data_ex = new List<ex_data>() {
                    new ex_data(){
                        id = 0,
                        info = AppStaticStr.SrvSingleCrud,
                        value = AppStaticStr.SrvSingleCrud
                        },
                    new ex_data(){
                        id = 1,
                        info = AppStaticStr.SrvTable,
                        value = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_company().company_tablename
                        },
                    new ex_data(){
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudGet
                        },
                    };

            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            cResponse response_get = mvcPost.post(AppStaticStr.urlRestRefCrud, model: Model);

            if (response_get.message_code != AppStaticInt.msg001Succes)
                return Ok(response);
            company e_cmp = JsonConvert.DeserializeObject<company>(response_get.data);

            e_cmp.company_name = bolucu[1];
            e_cmp.company_updatetime = DateTime.Now;

            cRequest e_tmp_cmp = new cRequest()
            {
                project_code = AppStaticInt.ProjectCodeCore,
                data = JsonConvert.SerializeObject(e_cmp),
                token = Model.token,
                data_ex = new List<ex_data>() {
                    new ex_data(){
                        id = 0,
                        info = AppStaticStr.SrvSingleCrud,
                        value = AppStaticStr.SrvSingleCrud
                        },
                    new ex_data(){
                        id = 1,
                        info = AppStaticStr.SrvTable,
                        value = new micro_services_dal.Models.zoradamlar_com_db_mic_user.info_company().company_tablename
                        },
                    new ex_data(){
                        id = 2,
                        info = AppStaticStr.SrvOpt,
                        value = AppStaticStr.SingleCrudSave
                        },
                    }
            };

            cResponse response_last = mvcPost.post(AppStaticStr.urlRestRefCrud, model: e_tmp_cmp);
            if (response_last.message_code == AppStaticInt.msg001Succes)
                response = response_last;


            return Ok(response);
        }

        public IActionResult savePass([FromBody] cRequest Model)
        {
            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token
            };

            string newPass = Model.data;
            HashSet<char> specialCharacters = new HashSet<char>() { '%', '$', '#', '\'', '\\', '"' };

            if (newPass.Length < 8)
            {
                response.message = AppStaticStr.msg0090Passboyutyeterlidegil;
                return Ok(response);
            }

            if (!(newPass.Any(char.IsLower)))
            {
                response.message = AppStaticStr.msg0092Passkucukharfyok;
                return Ok(response);

            }

            if (!(newPass.Any(char.IsUpper)))
            {
                response.message = AppStaticStr.msg0095Passbuyukharfyok;
                return Ok(response);
            }

            if (newPass.Any(specialCharacters.Contains))
            {
                response.message = AppStaticStr.msg0097PassOzelkaraktervar;
                return Ok(response);
            }

            Model.project_code = AppStaticInt.ProjectCodeCore;
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            cResponse response_get = mvcPost.post(AppStaticStr.urlRestUserinfo, model: Model);

            if (response_get.message_code == AppStaticInt.msg001Fail)
                return Ok(response);

            allofusers e_aou = JsonConvert.DeserializeObject<allofusers>(response_get.data);

            //PAss çağırma
            info_passofusers inf_passuser = new info_passofusers();
            cRequest req_pass_get = new cRequest()
            {
                data = string.Format("{0}={1}", inf_passuser.passofusers_passofusers_users_id, e_aou.users_id),
                project_code = AppStaticInt.ProjectCodeCore,
                token = Model.token,
                data_ex = new List<ex_data>()
                {
                    new ex_data() { id = 0, info = AppStaticStr.SrvSingleCrud, value = AppStaticStr.SrvSingleCrud},
                    new ex_data() { id = 1, info = AppStaticStr.SrvTable, value = inf_passuser.passofusers_tablename},
                    new ex_data() { id = 2, info = AppStaticStr.SrvOpt, value = AppStaticStr.SingleCrudGetAll}
                }
            };

            cResponse resp_pass = mvcPost.post(AppStaticStr.urlRestRefCrud, model: req_pass_get);
            if (resp_pass.message_code == AppStaticInt.msg001Fail)
                return Ok(response);

            List<passofusers> l_passou = JsonConvert.DeserializeObject<List<passofusers>>(resp_pass.data);
            if (l_passou.Count != 1)
                return Ok(response);

            passofusers e_passou = l_passou[0];
            //çağrılan entity deleted durumuna getildi.
            e_passou.deletedpassofusers_id = true;

            //trans kayıt için paket hazırlandı
            cRequest req_update_oldpass = new cRequest()
            {
                data = JsonConvert.SerializeObject(e_passou),
                project_code = AppStaticInt.ProjectCodeCore,
                token = Model.token,
                data_ex = new List<ex_data>()
                {
                    new ex_data() {id=0,info=AppStaticStr.SrvTransCrud,value=AppStaticStr.SrvSingleCrud},
                    new ex_data() {id=1,info=AppStaticStr.SrvTable,value=inf_passuser.passofusers_tablename},
                     new ex_data() {id=4,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudSave}
                }
            };

            //yen şifre bilgileri girildi
            passofusers e_new_passou = new passofusers()
            {
                deletedpassofusers_id = false,
                passofusers_active = true,
                passofusers_createtime = DateTime.Now,
                passofusers_expiretime = DateTime.Now.AddDays(90),
                passofusers_id=0,
                passofusers_users_id=e_passou.passofusers_users_id,
                passofusers_use=true,
                passofusers_pass=AppStaticMethod.strEncrypt(Model.data)
            };
            //yeni şifrede trans ile kayıt için hazırlandı
            cRequest req_save_newpass = new cRequest()
            {
                data = JsonConvert.SerializeObject(e_new_passou),
                project_code = AppStaticInt.ProjectCodeCore,
                token = Model.token,
                data_ex = new List<ex_data>()
                {
                    new ex_data() {id=0,info=AppStaticStr.SrvTransCrud,value=AppStaticStr.SrvSingleCrud},
                    new ex_data() {id=1,info=AppStaticStr.SrvTable,value=inf_passuser.passofusers_tablename},
                     new ex_data() {id=4,info=AppStaticStr.SrvOpt,value=AppStaticStr.SingleCrudSave}
                }
            };
            //Transaction kayıt için list içine eklenme işlemi
            List<cRequest> l_req = new List<cRequest>();
            l_req.Add(req_update_oldpass);
            l_req.Add(req_save_newpass);

            //trans kayıt son paketleme
            cRequest req_last = new cRequest()
            {
                project_code = AppStaticInt.ProjectCodeCore,
                token = Model.token,
                data = JsonConvert.SerializeObject(l_req)
            };

            cResponse resp_last = mvcPost.post(AppStaticStr.urlRestRefCrudTrans, model: req_last);
            if (resp_last.message_code == AppStaticInt.msg001Fail)
                return Ok(response);

            cRequest req_staticRef = new cRequest()
            {
                data = "",
                project_code = AppStaticInt.ProjectCodeCore,
                token = Model.token
            };

            //kayıt sonrasında user id server üzerinde statick tabloda güncellendi
            cResponse resp_staicref = mvcPost.post(AppStaticStr.urlRestStaticListRefresh, model: req_staticRef);
            if (resp_staicref.message_code == AppStaticInt.msg001Fail)
                return Ok(response);


            return Ok(resp_last);
        }

        public IActionResult forgetPass ([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response = new cResponse()
            {
                message_code = AppStaticInt.msg001Fail,
                message = AppStaticStr.msg0040Hata,
                token = Model.token,
                data = string.Empty
            };

            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestForgetPass, model: Model);

            return Ok(response);


        }
        //reflection örneği için aşağıda vardır.
        #region reflection örneği
        public IActionResult test1([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            lang lng = new lang()
            {
                lang_id = 0,
                deletedlang_id = false,
                lang_active = true,
                lang_country_id = 1,
                lang_use = true,
                lang_name = "English"
            };

            ex_data crd = new ex_data()
            {
                id = 0,
                info = AppStaticStr.SrvSingleCrud,
                value = AppStaticStr.SrvSingleCrud
            };

            ex_data tbl = new ex_data()
            {
                id = 1,
                info = AppStaticStr.SrvTable,
                value = "lang"
            };

            ex_data opt = new ex_data()
            {
                id = 2,
                info = AppStaticStr.SrvOpt,
                value = "Save"
            };

            List<ex_data> ver = new List<ex_data>();
            ver.Add(crd);
            ver.Add(tbl);
            ver.Add(opt);

            cResponse response = new cResponse();
            cRequest req = new cRequest()
            {
                data = JsonConvert.SerializeObject(lng),
                token = Model.token,
                data_ex = ver
            };
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(token: Model.token);


            response = mvcPost.post(AppStaticStr.urlRestRefCrud, model: req);

            return BadRequest(response);
        }

        public class lang
        {
            public long lang_id { get; set; }
            public string lang_name { get; set; }
            public long lang_country_id { get; set; }
            public bool deletedlang_id { get; set; }
            public bool lang_active { get; set; }
            public bool lang_use { get; set; }
        }
        #endregion reflection örneği
    }
}
