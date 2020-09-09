using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZaMvcV03.Models;

namespace ZaMvcV03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Test(VUserLogin mod)
        {
            if (mod.USERNAME == "Aa")
                mod.PASSWORD = "AAAAA";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region recaptha ayarları için yazılan kodların yoruma alınması
        //public HomeController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        //private readonly string _googleVerifyAddress = "https://www.google.com/recaptcha/api/siteverify";

        //private readonly string _googleRecaptchaSecret = "6LcUT8kZAAAAAGpERh4ciAMeGskiG34Ezs01xODv";
        //private readonly IHttpClientFactory _httpClientFactory;

        //[HttpGet]
        //public async Task<JsonResult> RecaptchaV3Vverify(string token)
        //{
        //    TokenResponse tokenResponse = new TokenResponse()
        //    {
        //        Success = false
        //    };

        //    using (var client = _httpClientFactory.CreateClient())
        //    {
        //        var response = await client.GetStringAsync($"{_googleVerifyAddress}?secret={_googleRecaptchaSecret}&response={token}");
        //        tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response);
        //    }

        //    return Json(tokenResponse);
        //}
        #endregion recaptha ayarları için yazılan kodların yoruma alınması
    }
}
