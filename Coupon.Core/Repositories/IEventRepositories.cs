using Coupon.Core.Entities;
using Coupon.Core.Event;

namespace Coupon.Core.Repositories
{
    public interface IEventRepositories<T> where T : class
    {
        Task<IEnumerable<Events<T>>> GetAllAsync();
        Task<IEnumerable<Events<T>>> GetByIdAsync(Guid DomainId);
        Task<IEnumerable<Events<T>>> GetByEventTypeAsync(string eventType);
        Task AddAsync(IReadOnlyCollection<Events<T>> events);
    }
}
