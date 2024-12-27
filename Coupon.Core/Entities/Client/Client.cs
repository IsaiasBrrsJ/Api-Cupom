
using Coupon.Core.Event;
using Coupon.Core.Externsion;

namespace Coupon.Core.Entities.Client
{
    public class Client : EntityBase
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
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

            if (!IsActive)
                throw new InvalidOperationException("Client was deactivate");

            IsActive = !IsActive;

            InsertEvent(@operator, reason);
        }
        public void AlterTypeClient(string reason, string @operator, ClientType clientType)
        {
            if (!IsActive && !Enum.IsDefined(typeof(ClientType), clientType))
                throw new InvalidOperationException("Enum incorreto");

            InsertEvent(@operator, reason);

            ClientType = clientType;
        }
       
        public void UpdateName(string name, string reason, string @operator)
        {
            string validation = String.Empty;

            if (validation.IsNullOrEmptyValues(name, reason, @operator))
                throw new InvalidOperationException("Name, reason and operator must be informed");


            InsertEvent(@operator, reason);
            Name = name;

        }

        public void UpdateEmail(string email, string reason, string @operator)
        {
            string validation = String.Empty;

            if (validation.IsNullOrEmptyValues(email, reason, @operator))
                throw new InvalidOperationException("Email, reason and operator must be informed");


            Email = email;
            InsertEvent(@operator, reason);

        }
        public void InsertEvent(string @operator, string reason)
        {
            @events.Add(Events<Client>.Factories.Create(Id, @operator, reason, nameof(Client)));
        }
        public void UpdatePhoneNumber(string phoneNumber, string reason, string @operator)
        {
            string validation = String.Empty;

            if (validation.IsNullOrEmptyValues(phoneNumber, reason, @operator))
                throw new InvalidOperationException("PhoneNumber, reason and operator must be informed");


            InsertEvent(@operator, reason);
            PhoneNumber = phoneNumber;

        }
        public static class Factories
        {
            public static Client Create(string name, string email, string phoneNumber, ClientType clientType)
            {
                return new Client
                {
                    Id = Guid.NewGuid(),
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
