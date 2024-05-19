using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Service.Interfaces.Shared;

public interface IPossibleSpecialtyService
{
    public Task<IEnumerable<PossibleSpecialtyEntity>> GetPossibleSpecialtyListAsync();
    public Task DeletePossibleSpecialtyAsync(Guid id);
    public Task CreatePossibleSpecialtyAsync(PossibleSpecialtyEntity entity);
}