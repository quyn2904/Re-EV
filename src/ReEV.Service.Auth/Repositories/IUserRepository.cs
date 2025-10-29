using ReEV.Service.Auth.Models;

namespace ReEV.Service.Auth.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailOrPhoneAsync(string email, string phone);
    }
}
