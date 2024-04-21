using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Service.Interfaces.Shared;

public interface IFacultyService
{
    public Task<IEnumerable<FacultyEntity>> GetFacultyListAsync();
    public Task DeleteFacultyAsync(Guid id);
    public Task<FacultyEntity> GetFacultyById(Guid id);
}