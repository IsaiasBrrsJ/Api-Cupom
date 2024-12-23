
using Coupon.Core.Abstractions;
using Coupon.Core.Entities.Client;

namespace Coupon.Application.Command.Discount;

public sealed record CreateDiscountCommand(decimal percent, string @operator, string reason, ClientType clientType) : ICommand;

