using ReEV.Service.Auth.Models;
using ReEV.Service.Auth.Repositories.Interfaces;

namespace ReEV.Service.Auth.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task<RefreshToken> CreateAsync(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResult<RefreshToken>> GetAllAsync(int page = 1, int pageSize = 10, string search = "")
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> UpdateAsync(Guid id, RefreshToken entity)
        {
            throw new NotImplementedException();
        }
    }
}
