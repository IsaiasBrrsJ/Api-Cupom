
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
    
        private static IList<Events<Client>> @events = new List<Events<Client>>();
    
        public IReadOnlyCollection<Events<Client>> @eventRead = @events.AsReadOnly();
        //EF
        private Client()
        {
        }
        public void Deactivate(string reason, string @operator)
        {
         
            IsActive = !IsActive;

            events.Add(Events<Client>.Factories.Create(Id, @operator, reason, nameof(Client)));
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
