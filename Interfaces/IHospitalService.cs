namespace HospitalMiddleware.Interfaces
{
    public interface IHospitalService
    {
        List<object> GetHosptialByName(string searchText);
    }
}
