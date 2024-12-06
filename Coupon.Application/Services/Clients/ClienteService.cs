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

        public async Task<IEnumerable<Client>> GetAllClients()
        {
           var clientes = await _clientRepositories.GetAllAsync();

            return clientes;
        }

        public async Task<Client> GetClientById(Guid id)
        {
           var cliente = await _clientRepositories.GetByIdAsync(id);

            return cliente;
        }

        public async Task<Guid> InsertClient(Client client)
        {
            var id = await _clientRepositories.AddAsync(client);
            await _clientEvent.AddAsync(client.eventRead);

            await _unitOfWork.Commit();

            return id;
        }

        public async Task UpdateEmail(Guid id, string email, string reason, string @operator)
        {
            if (!id.IsGuid())
                throw new InvalidOperationException("Id Incorrect");

            var client = await GetClientById(id);

            if (!client.Id.IsGuid() || client is null)
                await Task.CompletedTask;


            client!.UpdateEmail(email, reason, @operator);
            await _clientEvent.AddAsync(client.eventRead);

            await _unitOfWork.Commit();

            await Task.CompletedTask;
        }
        public async Task UpdateName(Guid id, string name, string reason, string @operator)
        {
            if (!id.IsGuid())
                throw new InvalidOperationException("Id Incorrect");

            var client = await GetClientById(id);

            if (!client.Id.IsGuid() || client is null)
                await Task.CompletedTask;


            client!.UpdateName(name, reason, @operator);
            await _clientEvent.AddAsync(client.eventRead);

            await _unitOfWork.Commit();

            await Task.CompletedTask;
        }
        public async Task UpdatePhoneNumber(Guid id, string phoneNumber, string reason, string @operator)
        {
            if (!id.IsGuid())
                throw new InvalidOperationException("Id Incorrect");

            var client = await GetClientById(id);

            if (!client.Id.IsGuid() || client is null)
                await Task.CompletedTask;


            client!.UpdateName(phoneNumber, reason, @operator);
            await _clientEvent.AddAsync(client.eventRead);

            await _unitOfWork.Commit();

            await Task.CompletedTask;
        }
    }
}
