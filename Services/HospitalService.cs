using HospitalMiddleware.Model;
using System.Security.Cryptography;

namespace HospitalMiddleware.Services
{
    public class HospitalService
    {
        readonly HospitalMiddlewareContext _dbContext;
        public HospitalService(HospitalMiddlewareContext hospitalMiddlewareContext)
        {
            _dbContext = hospitalMiddlewareContext;
        }
        public object GetHosptialByName(string plainText)
        {
            var hospital = _dbContext.Hospitals.FirstOrDefault(x => x.Name == plainText);

            return hospital;
        }
    }
}
