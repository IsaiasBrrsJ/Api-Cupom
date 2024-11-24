using Coupon.Core.Entities.Client;

namespace Coupon.Core.Services
{
    public interface IClientService
    {
        Task<Guid> InsertClient(Client client);
        Task UpdateClient(Client client);
        Task DeactivateClient(Client client);
        Task<IEnumerable<Client>> GetAllClients();
        Task<IEnumerable<Client>> GetClientById(Guid id);
    }
}
