using AuthMicroservice.Domain.Models;

namespace AuthMicroservice.Domain.Abstract.Services
{
    public interface IAuthService
    {
        string AnonUser();
        Task<User> Registration(User user);
        Task<User> Authenticate(User user, bool isPassword = true);
        Task<bool> IsUserLoginExist(string login);
    }
}
