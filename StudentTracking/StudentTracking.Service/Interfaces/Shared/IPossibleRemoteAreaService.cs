using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Service.Interfaces.Shared;

public interface IPossibleRemoteAreaService
{
    public Task<IEnumerable<PossibleRemoteAreaEntity>> GetPossibleRemoteAreaListAsync();
    public Task DeletePossibleRemoteAreaAsync(Guid id);
}