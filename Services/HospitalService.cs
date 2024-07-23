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
        public IEnumerable<object> GetHosptialByName(string plainText)
        {
            var hospital = _dbContext.Hospitals.Where(x => x.Name == plainText).ToList();

            return hospital;
        }
    }
}
