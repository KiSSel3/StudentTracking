using StudentTracking.Domain.Entity.Main;

namespace StudentTracking.Service.Interfaces.Main;

public interface IRoleService
{
    public Task<IEnumerable<RoleEntity>> GetAllAsync();
    public Task<RoleEntity> CreateAsync(string name);
    public Task<RoleEntity> UpdateAsync(Guid roleId, string name);
    public Task<RoleEntity> DeleteAsync(Guid roleId);
    public Task<RoleEntity> GetByIdAsync(Guid roleId);
    public Task<RoleEntity> GetByNameAsync(string name);
}