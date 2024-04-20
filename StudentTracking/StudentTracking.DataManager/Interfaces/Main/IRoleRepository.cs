using StudentTracking.Domain.Entities.Main;

namespace StudentTracking.DataManager.Interfaces.Main;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    public Task<RoleEntity> GetByNameAsync(string name);
}