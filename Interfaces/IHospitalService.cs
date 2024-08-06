using HospitalMiddleware.Model;

namespace HospitalMiddleware.Interfaces
{
    public interface IHospitalService
    {
        List<object> GetHosptialByName(string searchText);

        Hospital GetHospitalDetailsById(string Id);

        List<Hospital> GetActiveHospitals();
    }
}
