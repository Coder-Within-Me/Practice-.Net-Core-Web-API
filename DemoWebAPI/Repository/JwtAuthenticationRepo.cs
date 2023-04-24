using DemoWebAPI.DataLayer;
using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebAPI.Repository
{
    public class JwtAuthenticationRepo : IJwtAuthenticationRepo
    {
        private readonly TestContext _testContext;
        private readonly IConfiguration _configuration;
        public JwtAuthenticationRepo(TestContext context, IConfiguration Configuration)
        {
            _testContext = context;
            _configuration = Configuration;
        }

        public async Task<(string,bool)> UserLogin(string UserName, string Password)
        {
            var UserDetails = await _testContext.Users.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefaultAsync();
            if (UserDetails != null && UserDetails.UserId > 0)
            {
                var token = GenerateJWT(UserDetails.UserId.ToString(), UserDetails.UserName, UserDetails.Role);
                return (token,true);
            }
            else { return ("Please enter valid Username or Password.",false); }
        }

        public string GenerateJWT(string UserId, string UserName, string Role)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Sid, UserId),
                 new Claim(JwtRegisteredClaimNames.GivenName, UserName),
                 new Claim(ClaimTypes.Role,Role),
                 new Claim("Date", DateTime.Now.ToString()),
                 };

            var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"],
              _configuration["JwtConfig:Audiance"],
              claims,    //null original value
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            string Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
            return Data;
        }
    }
}
