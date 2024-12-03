using Coupon.Core.Entities.Client;

namespace Coupon.Application.InputModel.Clients
{
    public class ClientInputModel
    {
        public string Name { get; set; } = string.Empty;
        public string LasName { get; set; } = string.Empty;
        public int Age { get; private set; }
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = string.Empty;
        public ClientType ClientType { get; set; }


        public Client TOEntity()
        {
            return Client
                   .Factories
                   .Create(Name, Email, PhoneNumber, ClientType);
        }
    }
}
