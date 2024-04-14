using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Supporting;
using StudentTracking.Domain.Model.Main;

namespace StudentTracking.DataManager.Implementations.Supporting;

public class RoleUserRepository(ApplicationDbContext dbContext) : IRoleUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(UserRoleModel item)
    {
        await _dbContext.UsersRoles.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserRoleModel> UpdateAsync(UserRoleModel item)
    {
        _dbContext.UsersRoles.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(UserRoleModel item)
    {
        _dbContext.UsersRoles.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserRoleModel> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.UsersRoles.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<UserRoleModel>> GetAllAsync()
    {
        var list = await _dbContext.UsersRoles.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<UserRoleModel>> GetByUserId(Guid userId)
    {
        var list = await _dbContext.UsersRoles.Where(ur => ur.UserId == userId).ToListAsync();

        return list;
    }

    public async Task<IEnumerable<UserRoleModel>> GetByRoleId(Guid roleId)
    {
        var list = await _dbContext.UsersRoles.Where(ur => ur.RoleId == roleId).ToListAsync();
        
        return list;
    }
}