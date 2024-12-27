using Coupon.Application.Query.Client;
using Coupon.Application.ViewModel.Client;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Client;

public class GetAllClientsQueryHandler : IQueryHanlder<GetAllClients, ResultViewModel>
{
    private readonly IClientRepositories _clientRepositories;
    public GetAllClientsQueryHandler(IClientRepositories clientRepositories)
    {
        _clientRepositories = clientRepositories;
    }

    public async Task<ResultViewModel> Handler(GetAllClients query)
    {
        var result = await _clientRepositories.GetAllAsync();

        if (!result.Any())
            return ResultViewModel<IEnumerable<ClientViewModel>>.Success(Enumerable.Empty<ClientViewModel>(), "Empty");


        var clients = result.Select(x => ClientViewModel.Create(x.Email, x.PhoneNumber, Enum.GetName(typeof(ClientType), x.ClientType)!, x.IsActive)).ToList();

        return ResultViewModel<IEnumerable<ClientViewModel>>.Success(clients, "All Clietns");

    }
}
