using Coupon.Application.Command.Client;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Client;

public sealed record UpdateEmailCommandHandler : ICommandHandler<UpdateEmailCommand>
{
    private readonly IClientRepositories _clientRepositories;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventRepositories<Core.Entities.Client.Client> _clientEvent;

    public UpdateEmailCommandHandler(
        IClientRepositories clientRepositories, 
        IUnitOfWork unitOfWork, 
        IEventRepositories<Core.Entities.Client.Client> clientEvent)
    {
        _clientRepositories = clientRepositories;
        _unitOfWork = unitOfWork;
        _clientEvent = clientEvent;
    }

    public async Task<ResultViewModel> ExecuteAsync(UpdateEmailCommand command)
    {
        if (command == null || !command.id.IsGuid())
            return ResultViewModel.Failure("Incorrect id");

        var client = await _clientRepositories.GetByIdAsync(command.id);

        if (client is null)
            return ResultViewModel.Failure("Client Email not found");


        client.UpdateEmail(command.newEmail, command.reason, command.@operator);

        await _clientEvent.AddAsync(client.eventRead);

        await _unitOfWork.Commit();

        return ResultViewModel.Success("Accepted");
    }
}
