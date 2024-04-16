using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Interfaces.Letter;

public interface ILetterRepository : IBaseRepository<LetterEntity>
{
    public Task<IEnumerable<LetterEntity>> GetByFacultyIdAsync(Guid facultyId);
    public Task<IEnumerable<LetterEntity>> GetByCompanyIdAsync(Guid companyId);
}