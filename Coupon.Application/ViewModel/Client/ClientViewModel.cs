
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Client;

namespace Coupon.Application.ViewModel.Client
{
    public class ClientViewModel
    {
        public string Email { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public string ClientType { get; init; }
        public bool IsActive { get; init; }

        public static ClientViewModel Create(string email, string phoneNumber, string client, bool isActive)
        {
            return new ClientViewModel
            {
                Email = email,
                ClientType = client,
                PhoneNumber = phoneNumber,
                IsActive = isActive
            };
        }
    }
}
