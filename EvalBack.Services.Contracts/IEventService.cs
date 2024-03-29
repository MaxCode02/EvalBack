﻿using EvalBack.Entity;

namespace EvalBack.Services.Contracts
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(Event eventToCreate);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> UpdateEventAsync(int id, Event eventToUpdate);
        Task<bool> DeleteEventAsync(int Id);
    }
}