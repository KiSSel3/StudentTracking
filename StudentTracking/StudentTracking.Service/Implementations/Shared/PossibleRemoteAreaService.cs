using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Shared;

namespace StudentTracking.Service.Implementations.Shared;

public class PossibleRemoteAreaService(
    IBaseRepository<PossibleRemoteAreaEntity> possibleReomteAreaRepository,
    ILogger<CompanyService> logger)
    : IPossibleRemoteAreaService
{
    private readonly IBaseRepository<PossibleRemoteAreaEntity> _possibleReomteAreaRepository = possibleReomteAreaRepository;
    private readonly ILogger<CompanyService> _logger = logger;

    private void HandleError(string message)
    {
        _logger.LogError(message);
        
        throw new Exception(message);
    }
    
    public async Task<IEnumerable<PossibleRemoteAreaEntity>> GetPossibleRemoteAreaListAsync()
    {
        var list = await _possibleReomteAreaRepository.GetAllAsync();

        return list;
    }

    public async Task DeletePossibleRemoteAreaAsync(Guid id)
    {
        var item = await _possibleReomteAreaRepository.GetByIdAsync(id);
        if (item is null)
        {
            HandleError($"PossibleRemoteArea with id: {id} not found");
        }

        await _possibleReomteAreaRepository.DeleteAsync(item);
    }
}