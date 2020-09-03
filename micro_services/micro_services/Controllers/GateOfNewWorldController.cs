using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services.A00;
using micro_services.A00_Core;
using micro_services_share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace micro_services.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GateOfNewWorldController : ControllerBase
    {
        //public IActionResult Index()
        //{

        //}
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] cRequest model)
        {
            cResponse response = new classToken().Authenticate(model, ipAddress());


            if (AppStaticInt.msg0001WrongUserNamePass_i == response.message_code)
                    return BadRequest(response);

            //setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        //TODO:sonrasında gelen İP adreslere göre kısıtlama yada iptal işlemi yapılabilmesi için aşağıdaki method tutulacak
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
