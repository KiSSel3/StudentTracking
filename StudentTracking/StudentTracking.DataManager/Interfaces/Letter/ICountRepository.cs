using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Interfaces.Letter;

public interface ICountRepository : IBaseRepository<CountEntity>
{
    public Task<IEnumerable<CountEntity>> GetByLetterIdAsync(Guid letterId);
}