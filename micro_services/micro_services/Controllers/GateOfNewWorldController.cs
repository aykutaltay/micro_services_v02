using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using micro_services.A00;
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
            var response = _userService.Authenticate(model, ipAddress());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
