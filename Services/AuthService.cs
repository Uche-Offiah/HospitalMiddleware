using HospitalMiddleware.Interfaces;
using HospitalMiddleware.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalMiddleware.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public string Auth(string userName, string password)
        {
            var usern = _configuration["AdminUser"];
            var passw = _configuration["Password"];
            if (password.Equals(_configuration["Password"]) && userName.Equals(_configuration["AdminUser"]))
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var signingCredential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                var subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Email, userName),
                    });
                var expires = DateTime.Now.AddMinutes(10);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = expires,
                    Issuer = issuer,
                    Audience = audience,
                    Subject = subject,
                    SigningCredentials = signingCredential
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return jwtToken;
            }
            else
            {
                return null;
            }
        }
    }
}
