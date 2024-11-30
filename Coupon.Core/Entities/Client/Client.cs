
using Coupon.Core.Event;

namespace Coupon.Core.Entities.Client
{
    public class Client : EntityBase
    {
        public string Name { get; private set; } = default!;
        public string Email { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public ClientType ClientType { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreateDate {  get; private set; }
       
        //EF
        private Client()
        {
        }
        public void Deactivate()
        {
         

            IsActive = !IsActive;
        }
        public void AlterTypeClient(ClientType clientType)
        {

            ClientType = clientType;
        }
       
        public static class Factories
        {
            public static Client Create(string name, string email, string phoneNumber, ClientType clientType)
            {
                return new Client
                {
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    ClientType = clientType,
                    IsActive = true,
                    CreateDate = DateTime.UtcNow
                };
            }
          
        }
    }
}
