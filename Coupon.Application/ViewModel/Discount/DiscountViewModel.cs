
namespace Coupon.Application.ViewModel.Discount;

public sealed record DiscountViewModel(string clientType, decimal percent, DateTime creatAt, bool IsActive);
