using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Shared;

namespace StudentTracking.Service.Implementations.Shared;

public class PossibleSpecialtyService : IPossibleSpecialtyService
{
    private readonly IBaseRepository<PossibleSpecialtyEntity> _possibleSpecialtyRepository;
    private readonly ILogger<CompanyService> _logger;

    private void HandleError(string message)
    {
        _logger.LogError(message);
        
        throw new Exception(message);
    }
    
    public async Task<IEnumerable<PossibleSpecialtyEntity>> GetPossibleSpecialtyListAsync()
    {
        var list = await _possibleSpecialtyRepository.GetAllAsync();

        return list;
    }

    public async Task DeletePossibleSpecialtyAsync(Guid id)
    {
        var item = await _possibleSpecialtyRepository.GetByIdAsync(id);
        if (item is null)
        {
            HandleError($"PossibleRemoteArea with id: {id} not found");
        }

        await _possibleSpecialtyRepository.DeleteAsync(item);
    }
}