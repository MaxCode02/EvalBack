using EvalBack.Entity;

namespace EvalBack.Repository.Contracts
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event eventToAdd);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task<Event> UpdateAsync(int id,Event eventToUpdate);
        Task<bool> DeleteAsync(int id);
        
    }

}