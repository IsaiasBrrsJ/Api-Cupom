
namespace Coupon.Application.InputModel.Clients;

public sealed record class UpdatePhoneNumber(string phoneNumber, string reason, string @operator);