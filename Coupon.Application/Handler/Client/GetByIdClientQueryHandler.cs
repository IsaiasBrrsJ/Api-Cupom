using Coupon.Application.Extension;
using Coupon.Application.Query.Client;
using Coupon.Application.ViewModel.Client;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Client;

public class GetByIdClientQueryHandler : IQueryHanlder<GetByIdClient, ResultViewModel>
{
    private readonly IClientRepositories _clientRepositories;
    public GetByIdClientQueryHandler(IClientRepositories clientRepositories)
    {
        _clientRepositories = clientRepositories;
    }

    public async Task<ResultViewModel> Handler(GetByIdClient query)
    {
        var result = await _clientRepositories.GetByIdDapperAsync(query.id);

        if (result == null || !result.Id.IsGuid())
            return ResultViewModel<ClientViewModel>.Failure(null, $"{query.id}  Not Found");

        var viewModel = ClientViewModel.Create(result.Email, result.PhoneNumber, Enum.GetName(typeof(ClientType), result.ClientType)!, result.IsActive);

        return ResultViewModel<ClientViewModel>.Success(viewModel, "Success");
    }
}
