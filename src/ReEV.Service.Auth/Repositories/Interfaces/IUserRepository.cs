using ReEV.Service.Auth.DTOs;
using ReEV.Service.Auth.Models;

namespace ReEV.Service.Auth.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserDTO?> GetByEmailOrPhoneAsync(string identifier);
    }
}
