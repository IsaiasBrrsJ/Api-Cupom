
using Coupon.Application.Command.Client;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Clients;


public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
{
    private readonly IClientRepositories _clientRepositories;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventRepositories<Core.Entities.Client.Client> _clientEvent;

    public CreateClientCommandHandler(
        IClientRepositories clientRepositories, 
        IUnitOfWork unitOfWork, 
        IEventRepositories<Core.Entities.Client.Client> clientEvent)
    {
        _clientRepositories = clientRepositories;
        _unitOfWork = unitOfWork;
        _clientEvent = clientEvent;
    }

    public async Task<ResultViewModel> ExecuteAsync(CreateClientCommand command)
    {
        var client = Core.Entities.Client.Client.Factories.Create(command.name, command.email, command.phoneNumber, command.clientType);
        var id = await _clientRepositories.AddAsync(client);
        
        client.InsertEvent(command.@operator, command.reason);
        
        await _clientEvent.AddAsync(client.eventRead);

        await _unitOfWork.Commit();

        return ResultViewModel.Success($"{id}");
    }
}
