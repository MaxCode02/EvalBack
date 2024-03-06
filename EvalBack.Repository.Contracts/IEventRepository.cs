using EvalBack.Entity;

namespace EvalBack.Repository.Contracts
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event eventToAdd);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(Guid id);
        Task<Event> UpdateAsync(Event eventToUpdate);
        Task<bool> DeleteAsync(Guid id);
        
    }

}