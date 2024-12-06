namespace Coupon.Application.InputModel.Clients;

public sealed record class UpdateEmail(string email, string reason, string @operator);