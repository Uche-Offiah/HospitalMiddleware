﻿using HospitalMiddleware.Interfaces;
using HospitalMiddleware.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalMiddleware.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Auth/{userName}/{password}")]
        public ActionResult Auth(string userName, string password)
        {
            GenericResponse response = new GenericResponse();
            if (userName != null && password != null)
            {
                var usern = _configuration["AdminUser"];
                var passw = _configuration["Password"];
                if (password.Equals(_configuration["Password"]) && userName.Equals(_configuration["AdminUser"]))
                {
                    //var issuer = _configuration["Jwt:Issuer"];
                    //var audience = _configuration["Jwt:Audience"];
                    //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                    //var signingCredential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                    //var subject = new ClaimsIdentity(new[]
                    //{
                    //new Claim(JwtRegisteredClaimNames.Sub, userName),
                    //new Claim(JwtRegisteredClaimNames.Email, userName),
                    //});
                    //var expires = DateTime.Now.AddMinutes(10);

                    //var tokenDescriptor = new SecurityTokenDescriptor
                    //{
                    //    Expires = expires,
                    //    Issuer = issuer,
                    //    Audience = audience,
                    //    Subject = subject,
                    //    SigningCredentials = signingCredential
                    //};

                    //var tokenHandler = new JwtSecurityTokenHandler();
                    //var token = tokenHandler.CreateToken(tokenDescriptor);
                    //var jwtToken = tokenHandler.WriteToken(token);

                    var jwtToken = _authService.Auth(userName, password);

                    response.StatusCode = 200;
                    response.Data = jwtToken;
                    return Ok(response);
                }
            }

            response.StatusCode = 401;
            response.Data = "Incorrect Username or Password";

            return Ok(response);

        }
    }
}

