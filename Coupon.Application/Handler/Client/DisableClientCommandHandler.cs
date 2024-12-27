using Coupon.Application.Command.Client;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Client;

public class DisableClientCommandHandler : ICommandHandler<DisableClientCommand>
{
    private readonly IClientRepositories _clientRepositories;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventRepositories<Core.Entities.Client.Client> _clientEvent;

    public DisableClientCommandHandler(
        IClientRepositories clientRepositories, 
        IUnitOfWork unitOfWork, 
        IEventRepositories<Core.Entities.Client.Client> clientEvent)
    {
        _clientRepositories = clientRepositories;
        _unitOfWork = unitOfWork;
        _clientEvent = clientEvent;
    }

    public async Task<ResultViewModel> ExecuteAsync(DisableClientCommand command)
    {
        if (!command.id.IsGuid())
            throw new InvalidOperationException("Id Incorrect");


        var client = await _clientRepositories.GetByIdAsync(command.id);

        if (!client.Id.IsGuid() || client is null)
            return ResultViewModel.Failure("client not found");

        client!.Deactivate(command.reason,command.@operator);

        await _clientEvent.AddAsync(client!.eventRead);


        await _unitOfWork.Commit();


        return ResultViewModel.Success("Accepted");
    }
}
