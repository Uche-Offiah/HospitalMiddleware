using HospitalMiddleware.Interfaces;
using HospitalMiddleware.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalMiddleware.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public GenericResponse Auth(string userName, string password)
        {
            
        }
    }
}
