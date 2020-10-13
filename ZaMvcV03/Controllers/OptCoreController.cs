using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services_share;
using micro_services_share.Model;
using micro_services_share.vModel;
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

        public IActionResult MVCNewUser ([FromBody] cRequest Model)
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

        public IActionResult nuser ([FromBody] cRequest Model)
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

        public IActionResult tokenRenew ([FromBody] cRequest Model)
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

            allofusers e_aou = JsonConvert.DeserializeObject<allofusers>(response.data);
            response.message_code = AppStaticInt.msg001Fail;
            response.message = AppStaticStr.msg0040Hata;
            DateTime retokenTime = e_aou.tokensofusers_expiretime.AddMinutes(-60);

            if (retokenTime<DateTime.Now)
            {
                response = mvcPost.post(AppStaticStr.urlRestNewToken, model: Model);
            }


            return Ok(response);
        }

        public IActionResult saveUser ([FromBody] cRequest Model)
        {
            Model.project_code = AppStaticInt.ProjectCodeCore;

            cResponse response=new AppClassMVCMethod(Model.token).SaveUser(Model);


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
