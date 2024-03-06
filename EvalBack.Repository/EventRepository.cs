using EvalBack.DAL;
using EvalBack.Entity;
using EvalBack.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;


namespace EvalBack.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly EvalBackDbContext _context; 

        public EventRepository(EvalBackDbContext context)
        {
            _context = context;
        }

        public async Task<Event> AddAsync(Event eventToAdd)
        {
            await _context.Events.AddAsync(eventToAdd);
            await _context.SaveChangesAsync();
            return eventToAdd;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> UpdateAsync(int id ,Event eventToUpdate)
        {
            _context.Events.Update(eventToUpdate);
            await _context.SaveChangesAsync();
            return eventToUpdate;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var eventToRemove = await _context.Events.FindAsync(id);
            if (eventToRemove != null)
            {
                _context.Events.Remove(eventToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }


}