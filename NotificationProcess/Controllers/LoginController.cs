using DenemeHataBildirimi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DenemeHataBildirimi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("token")]
        public IActionResult Login(string username, string pass)
        {
            UserModel login = new UserModel();
            login.UserName = username;
            login.Password = pass;
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }
        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            //For demo I am using satic user Info
            if (login.UserName == "ludens" && login.Password == "1234")
            {
                user = new UserModel { UserName = "LudensSes", EmailAdress = "ses@Ludens.com", Password = "123" };
            }
            return user;
        }
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub,userInfo.UserName),
                new Claim (JwtRegisteredClaimNames.Email,userInfo.EmailAdress),
                new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
