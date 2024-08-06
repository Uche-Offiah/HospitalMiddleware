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

        public Hospital GetHospitalDetailsById(string Id)
        {
            var hospital = _dbContext.Hospitals.Where(x => x.Name == Id).FirstOrDefault();

            return hospital;
        }

        public Hospital GetActiveHospitals()
        {
            var hospital = _dbContext.Hospitals.Where(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault();

            return hospital;
        }       
    }
}
