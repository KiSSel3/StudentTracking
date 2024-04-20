using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Main;
using StudentTracking.Domain.Entities.Main;

namespace StudentTracking.DataManager.Implementations.Main;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(UserEntity item)
    {
        await _dbContext.Users.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserEntity> UpdateAsync(UserEntity item)
    {
        _dbContext.Users.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(UserEntity item)
    {
        _dbContext.Users.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Users.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var list = await _dbContext.Users.ToListAsync();

        return list;
    }

    public async Task<UserEntity> GetByLoginAsync(string login)
    {
        var item = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);

        return item;
    }
}