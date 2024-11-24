using Coupon.Core.Entities.Client;

namespace Coupon.Core.Services
{
    public interface IClientRepositories
    {
        Task<Guid> AddAsync(Client client);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<IEnumerable<Client>> GetByIdAsync(Guid id);
        Task UpdateAsync(Client client);
        Task DeleteLogicalAsync(Guid id);
    }
}
