namespace rescomac_back.business.Interface
{
    public interface ILoginService
    {
        Task<bool> ValidarUsuario(string email, string password);
    }
}