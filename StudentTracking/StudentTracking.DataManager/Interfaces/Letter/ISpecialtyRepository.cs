using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Interfaces.Letter;

public interface ISpecialtyRepository : IBaseRepository<SpecialtyEntity>
{
    public Task<IEnumerable<SpecialtyEntity>> GetByLetterIdAsync(Guid letterId);
}