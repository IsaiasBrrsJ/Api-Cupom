using Coupon.Core.Event;
using Coupon.Core.Repositories;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class EventRepositories<T> : IEventRepositories<T> where T : class
    {
        private readonly CouponContextDb _contextDb;

        public EventRepositories(CouponContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task AddAsync(IReadOnlyCollection<Events<T>> events)
        {
            foreach(var @event in events)
            {
                await _contextDb.AddAsync(@event);
            }
           
           
            await Task.CompletedTask;
        }

        public Task<IEnumerable<Events<T>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Events<T>>> GetByEventTypeAsync(string eventType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Events<T>>> GetByIdAsync(Guid DomainId)
        {
            throw new NotImplementedException();
        }
    }
}
