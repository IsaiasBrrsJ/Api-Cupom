using Coupon.Core.Entities.Client;

namespace Coupon.Core.Services
{
    public interface IClientService
    {
        Task<Guid> InsertClient(Client client);
        Task UpdateClient(Client client);
        Task DeactivateClient(Guid id, string reason, string @operator);
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(Guid id);
    }
}
