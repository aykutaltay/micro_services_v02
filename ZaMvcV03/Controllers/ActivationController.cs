using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZaMvcV03.Models;

namespace ZaMvcV03.Controllers
{
    public class ActivationController : Controller
    {
        //private static readonly HttpClient client = new HttpClient();

        [HttpGet ("ActivationActive")]
        public IActionResult Activation(string actkey)
        {

            if (actkey==null)
            {
                ViewBag.msg = "Aktivasyon Key Hatası";
                return View();
            }    
            cResponse response = new cResponse();
            //var resp = client.GetAsync("https://localhost:5001/GateOfNewWorld/activation?acktKey=" + actkey);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);
            //var resp = client.GetAsync("https://localhost:5001/GateOfNewWorld/activation?actkey=" + actkey);
            var fromBody = new StringContent(actkey, Encoding.UTF8, "application/json");

            var downloadTask = client.PostAsync("https://localhost:5001/GateOfNewWorld/activation?actkey=" + actkey, fromBody);
            downloadTask.Wait();
            var downloadResult = downloadTask.Result;
            var stringTask = downloadResult.Content.ReadAsStringAsync();
            stringTask.Wait();

            response = JsonConvert.DeserializeObject<cResponse>(stringTask.Result);
            ViewBag.msg = response.message;
            return View();
        }
    }
}
