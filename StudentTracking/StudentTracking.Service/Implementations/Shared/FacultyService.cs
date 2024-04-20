using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Shared;

namespace StudentTracking.Service.Implementations.Shared;

public class FacultyService(IBaseRepository<FacultyEntity> facultyRepository, ILogger<FacultyService> logger)
    : IFacultyService
{
    private readonly IBaseRepository<FacultyEntity> _facultyRepository = facultyRepository;
    private readonly ILogger<FacultyService> _logger = logger;

    private void HandleError(string message)
    {
        _logger.LogError(message);
        
        throw new Exception(message);
    }
    
    public async Task<IEnumerable<FacultyEntity>> GetFacultyListAsync()
    {
        var faculty = await _facultyRepository.GetAllAsync();

        return faculty;
    }

    public async Task DeleteFacultyAsync(Guid id)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id);
        if (faculty is null)
        {
            HandleError($"Company with id: {id} not found");
        }

        await _facultyRepository.DeleteAsync(faculty);
    }
}