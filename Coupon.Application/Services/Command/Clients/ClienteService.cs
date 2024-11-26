﻿using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;
using Coupon.Core.Services;

namespace Coupon.Application.Services.Command.Clients
{
    public class ClienteService : IClientService
    {
        private readonly IClientRepositories _clientRepositories;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IClientRepositories clientRepositories, IUnitOfWork unitOfWork)
        {
            _clientRepositories = clientRepositories;
            _unitOfWork = unitOfWork;
        }

        public async Task DeactivateClient(Client client)
        {

            await _clientRepositories.AddAsync(client);

            await _unitOfWork.Commit();
        }

        public Task<IEnumerable<Client>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetClientById(Guid id)
        {
            throw new NotImplementedException();
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
