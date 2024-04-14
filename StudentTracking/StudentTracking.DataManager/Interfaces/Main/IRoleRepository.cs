using StudentTracking.Domain.Entity.Main;

namespace StudentTracking.DataManager.Interfaces.Main;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    public Task<RoleEntity> GetByNameAsync(string name);
}