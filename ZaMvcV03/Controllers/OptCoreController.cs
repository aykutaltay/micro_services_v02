using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services_share;
using micro_services_share.Model;
using Microsoft.AspNetCore.Mvc;
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
                message_code = AppStaticInt.msg0001WrongUserNamePass_i,
                message = AppStaticStr.msg0001WrongUserNamePass,
                token = string.Empty,
                data = string.Empty
            };

            return Ok(response);
        }

        public IActionResult auth([FromBody] cRequest Model)
        {
            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestAuth, model: Model);

            return Ok(response);
        }

        public IActionResult nuser ([FromBody] cRequest Model)
        {
            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestAuth, model: Model);

            return Ok(response);
        }

        public IActionResult mainuserlist([FromBody] cRequest Model)
        {
            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestMainUserList, model: Model);

            return Ok(response);
        }

        public IActionResult mainuserget([FromBody] cRequest Model)
        {
            cResponse response = new cResponse();
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(Model.token);
            response = mvcPost.post(AppStaticStr.urlRestMainUserGet, model: Model);

            return Ok(response);
        }


    }
}
