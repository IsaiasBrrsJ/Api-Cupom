using Coupon.Core.Entities.Client;

namespace Coupon.Core.Repositories
{
    public interface IClientRepositories
    {
        Task<Guid> AddAsync(Client client);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(Guid id);
        Task UpdateAsync(Client client);
        Task DeleteLogicalAsync(Guid id);
    }
}
