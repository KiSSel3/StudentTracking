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

        faculty.IsDeleted = true;
        
        await _facultyRepository.UpdateAsync(faculty);
    }

    public async Task<FacultyEntity> GetFacultyById(Guid id)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id);
        if (faculty is null)
        {
            HandleError($"Company with id: {id} not found");
        }

        return faculty;
    }

    public async Task UpdateFaculty(FacultyEntity entity)
    {
        var faculty = await _facultyRepository.GetByIdAsync(entity.Id);
        if (faculty is null)
        {
            HandleError($"Company with id: {entity.Id} not found");
        }

        faculty.FullName = entity.FullName;
        faculty.Abbreviation = entity.Abbreviation;
        
        await _facultyRepository.UpdateAsync(faculty);
    }

    public async Task CreateFaculty(FacultyEntity entity)
    {
        await _facultyRepository.CreateAsync(entity);
    }
}