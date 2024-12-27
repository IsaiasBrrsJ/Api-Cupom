
using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Client;

public sealed record UpdatePhoneNumberCommand(Guid id, string reason, string @operator, string newPhoneNumber) : ICommand;
