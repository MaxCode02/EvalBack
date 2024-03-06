using EvalBack.Entity;

namespace EvalBack.Services.Contracts
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(Event eventToCreate);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> UpdateEventAsync( Event eventToUpdate);
        Task<bool> DeleteEventAsync(Guid Id);
    }
}