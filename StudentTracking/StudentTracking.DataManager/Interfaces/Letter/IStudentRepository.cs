using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Interfaces.Letter;

public interface IStudentRepository : IBaseRepository<StudentEntity>
{
    public Task<IEnumerable<StudentEntity>> GetByLetterIdAsync(Guid letterId);
}