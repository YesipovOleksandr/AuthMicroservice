using AuthMicroservice.Domain.Models;

namespace AuthMicroservice.Domain.Abstract.Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(long Id);
        Task<User> GetByLogin(string Login);
        Task<User> Create(User item);
        Task Update(User item);
        Task Save();
    }
}
