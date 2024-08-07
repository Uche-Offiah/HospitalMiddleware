using HospitalMiddleware.Model;

namespace HospitalMiddleware.Interfaces
{
    public interface IPatientService
    {
        List<Patient> GetPatientByName(string searchText);
        Patient GetPatientById(int searchText);
        List<object> GetPatientDetail(string searchText);
        List<Patient> GetActiveHospitals();
    }
}
