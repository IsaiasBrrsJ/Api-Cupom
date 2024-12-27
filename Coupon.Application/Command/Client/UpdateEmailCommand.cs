
using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Client;

public sealed record UpdateEmailCommand(Guid id, string reason, string @operator, string newEmail) : ICommand;
