using Coupon.Core.Entities.Coupon;
using Coupon.Core.Entities.Reason;

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
        public virtual ICollection<Description>? Descriptions { get; init; }
      


        public void Deactivate()
        {

            HasDescription();

            IsActive = !IsActive;
        }

        public void AlterTypeClient(ClientType clientType)
        {
            HasDescription();

            ClientType = clientType;
        }

        private void HasDescription()
        {
            if (Descriptions == null)
                throw new InvalidOperationException("Informe a razao da modificação");
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
                    IsActive = true
                };
            }
        }

    }
}
