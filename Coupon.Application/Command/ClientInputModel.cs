using Microsoft.AspNetCore.Http;

namespace Coupon.Application.Command
{
    public class ClientInputModel
    {
        public string Name { get; set; } = string.Empty;    
        public string LasName { get; set; } = string.Empty;
        public int Age { get; private set; } 
        public DateTime DateoOfBirth { get; set; } 
      
    }
}
