using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Implementations.Letter;

public class RemoteAreaRepository(ApplicationDbContext dbContext) : IRemoteAreaRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task CreateAsync(RemoteAreaEntity item)
    {
        await _dbContext.RemoteAreas.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RemoteAreaEntity> UpdateAsync(RemoteAreaEntity item)
    {
        _dbContext.RemoteAreas.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(RemoteAreaEntity item)
    {
        _dbContext.RemoteAreas.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RemoteAreaEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.RemoteAreas.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<RemoteAreaEntity>> GetAllAsync()
    {
        var list = await _dbContext.RemoteAreas.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<RemoteAreaEntity>> GetByLetterIdAsync(Guid letterId)
    {
        var list = await _dbContext.RemoteAreas.Where(ra => ra.LetterId == letterId).ToListAsync();
        
        return list;
    }
}