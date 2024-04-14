using StudentTracking.Domain.Model.Main;

namespace StudentTracking.DataManager.Interfaces.Supporting;

public interface IRoleUserRepository : IBaseRepository<UserRoleModel>
{
    public Task<IEnumerable<UserRoleModel>> GetByUserId(Guid userId);
    public Task<IEnumerable<UserRoleModel>> GetByRoleId(Guid roleId);
}