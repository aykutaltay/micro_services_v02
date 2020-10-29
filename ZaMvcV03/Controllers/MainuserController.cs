using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using micro_services_share;
using micro_services_share.Model;
using micro_services_share.vModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
//using ZaMvcV03.Models;

namespace ZaMvcV03.Controllers
{
    public class MainuserController : Controller
    {
        public IActionResult Menu()
        {

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Company()
        {
            return View();
        }

        public IActionResult ChangePass()
        {
            return View();
        }

        public IActionResult MenuEntry(string token)
        {

            if (token == null)
                return View("Index");

            cResponse response = new cResponse();
            cRequest req = new cRequest()
            {
                data = token,
                token = token,
                project_code=AppStaticInt.ProjectCodeCore
            };
            AppClassMVCMethod mvcPost = new AppClassMVCMethod(token:token);


            response=mvcPost.post(AppStaticStr.urlRestUserinfo,model: req);

            #region Methoda taşımadan önce yapılan uzun kod yazımımı yoruma alınması
            //HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //HttpClient client = new HttpClient(clientHandler);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var fromBody = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            //var downloadTask = client.PostAsync("https://localhost:5001/GateOfNewWorld/userinfo", fromBody);
            //downloadTask.Wait();
            //var downloadResult = downloadTask.Result;
            //var stringTask = downloadResult.Content.ReadAsStringAsync();
            //stringTask.Wait();

            //response = JsonConvert.DeserializeObject<cResponse>(stringTask.Result);
            #endregion

            if (response.message_code == AppStaticInt.msg001Succes)
            {
                allofusers aou = JsonConvert.DeserializeObject<allofusers>(response.data);

                if (aou.role_intvalue <= 100)
                {
                    response.message_code = AppStaticInt.msg001Succes;
                    return Ok(response);
                }
                else
                {
                    response.message_code = AppStaticInt.msg001Fail;
                    return BadRequest(response);
                }
            }




            return BadRequest(response);
        }



    }
}
