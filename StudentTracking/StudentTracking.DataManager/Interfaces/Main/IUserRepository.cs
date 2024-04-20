using StudentTracking.Domain.Entities.Main;

namespace StudentTracking.DataManager.Interfaces.Main;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<UserEntity> GetByLoginAsync(string login);
}