
using Coupon.Core.Abstractions;
using Coupon.Core.Entities.Client;

namespace Coupon.Application.Command.Client;

public sealed record CreateClientCommand(string @operator, string reason, string name, string email, string phoneNumber, ClientType clientType) : ICommand;

