using DemoWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationRepo JwtAuthObj;
        public AuthenticationController (IJwtAuthenticationRepo _JwtAuthObj)
        {
            JwtAuthObj = _JwtAuthObj;
        }

        [HttpGet("UserLogin/{UserName}/{Password}")]
        public async Task<IActionResult> UserLogin([FromRoute] string UserName, [FromRoute] string Password)
        {
            var data = await JwtAuthObj.UserLogin(UserName, Password);
            //if (data.Item2)
            //{
            //    HttpContext.Session.SetString("username", UserName);
            //    string test = HttpContext.Session.GetString("username");
            //}
            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpGet]
        public async Task<IActionResult> IsUserLoggedIn()
        {
            string token = Request.Headers["Authorization"];
            if (token != null)
            {
                token = token.Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                bool flag = handler.CanReadToken(token);
                if (flag)
                {
                    var tokendata = handler.ReadToken(token) as JwtSecurityToken;
                    string username = tokendata.Claims.First(x => x.Type == "given_name").Value;
                    //string UserName = HttpContext.Session.GetString("username");
                    if (username.Length > 0)
                    {
                        return Ok(true);
                    }
                }
                return Ok(false);
            }
            return Ok(false);
        }
    }
}
