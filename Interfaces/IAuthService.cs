namespace HospitalMiddleware.Interfaces
{
    public interface IAuthService
    {
        string Auth(string userName, string password);
    }
}
