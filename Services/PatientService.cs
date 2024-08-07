using HospitalMiddleware.Model;

namespace HospitalMiddleware.Services
{
    public class PatientService
    {
        readonly HospitalMiddlewareContext _dbContext;
        public PatientService(HospitalMiddlewareContext hospitalMiddlewareContext)
        {
            _dbContext = hospitalMiddlewareContext;
        }
        public IEnumerable<Patient> GetPatientByName(string plainText)
        {
            var patients = _dbContext.Patients.Where(x => x.LastName == plainText || x.LastName == plainText).ToList();

            return patients;
        }

        public IEnumerable<Patient> GetPatientByDetailsById(string plainText)
        {
            var patients = _dbContext.Patients.Where(x => x.LastName == plainText || x.LastName == plainText).ToList();

            return patients;
        }
        

        public Patient GetPatientById(int plainText)
        {
            var patients = _dbContext.Patients.Where(x => x.Id == plainText).FirstOrDefault();

            return patients;
        }

        public Patient GetActivePatients()
        {
            var patients = _dbContext.Patients.Where(x => x.IsActive == true && x.IsDeleted == false).FirstOrDefault();

            return patients;
        }
    }
}
