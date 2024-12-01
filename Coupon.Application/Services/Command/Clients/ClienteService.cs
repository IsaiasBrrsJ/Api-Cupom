using Coupon.Application.Extension;
using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;
using Coupon.Core.Services;

namespace Coupon.Application.Services.Command.Clients
{
    public class ClienteService : IClientService
    {
        private readonly IClientRepositories _clientRepositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepositories<Client> _clientEvent;
        public ClienteService(IClientRepositories clientRepositories, IUnitOfWork unitOfWork, IEventRepositories<Client> clientEvent)
        {
            _clientRepositories = clientRepositories;
            _unitOfWork = unitOfWork;
            _clientEvent = clientEvent;
        }

        public async Task DeactivateClient(Guid id, string reason, string @operator)
        {
            if (!id.IsGuid())
                throw new InvalidOperationException("Id Incorrect");


            var client = await GetClientById(id);

            if (!client.Id.IsGuid() || client is null)
                await Task.CompletedTask;


            client!.Deactivate(reason, @operator);

            await _clientEvent.AddAsync(client!.eventRead);

            await _unitOfWork.Commit();
        }

        public Task<IEnumerable<Client>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public async Task<Client> GetClientById(Guid id)
        {
           var cliente = await _clientRepositories.GetByIdAsync(id);

            return cliente;
        }

        public async Task<Guid> InsertClient(Client client)
        {
            var id = await _clientRepositories.AddAsync(client);

            await _unitOfWork.Commit();

            return id;
        }

        public Task UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
