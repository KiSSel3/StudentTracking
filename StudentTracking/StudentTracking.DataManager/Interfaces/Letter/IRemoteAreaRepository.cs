using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Interfaces.Letter;

public interface IRemoteAreaRepository : IBaseRepository<RemoteAreaEntity>
{
    public Task<IEnumerable<RemoteAreaEntity>> GetByLetterIdAsync(Guid letterId);
}