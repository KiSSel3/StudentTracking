using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Main;
using StudentTracking.Domain.Entity.Main;

namespace StudentTracking.DataManager.Implementations.Main;

public class RoleRepository(ApplicationDbContext dbContext) : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task CreateAsync(RoleEntity item)
    {
        await _dbContext.Roles.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RoleEntity> UpdateAsync(RoleEntity item)
    {
        _dbContext.Roles.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(RoleEntity item)
    {
        _dbContext.Roles.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RoleEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Roles.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        var list = await _dbContext.Roles.ToListAsync();

        return list;
    }

    public async Task<RoleEntity> GetByNameAsync(string name)
    {
        var item = await _dbContext.Roles.FirstOrDefaultAsync(role => role.Name == name);

        return item;
    }
}