using Coupon.Core.Entities.Client;

namespace Coupon.Core.Services
{
    public interface IClientService
    {
        Task<Guid> InsertClient(Client client);
        Task DeactivateClient(Guid id, string reason, string @operator);
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(Guid id);
        Task UpdateName(Guid id,string name, string reason, string @operator);
        Task UpdatePhoneNumber(Guid id,string phoneNumber, string reason, string @operator);
        Task UpdateEmail(Guid id,string email, string reason, string @operator);
    }
}
