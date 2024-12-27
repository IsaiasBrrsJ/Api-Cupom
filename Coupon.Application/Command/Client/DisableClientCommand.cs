using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Client;

public sealed record DisableClientCommand(Guid id, string reason, string @operator) : ICommand;
