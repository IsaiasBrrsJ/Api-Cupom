using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Client;

public sealed record UpdateNameCommand(Guid id, string reason, string @operator, string newName) :ICommand;
