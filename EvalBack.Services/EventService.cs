using EvalBack.Entity;
using EvalBack.Repository.Contracts;
using EvalBack.Services.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace EvalBack.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event> CreateEventAsync(Event eventToCreate)
        {
            
            return await _eventRepository.AddAsync(eventToCreate);
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
        
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event> UpdateEventAsync( Event eventToUpdate)
        {
          
            return await _eventRepository.UpdateAsync( eventToUpdate);
        }
        
        public async Task<bool> DeleteEventAsync(Guid Id)
        {
            
            return await _eventRepository.DeleteAsync(Id);
        }
    }


}