using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.DataManager.Implementations.Shared;

public class PossibleRemoteAreaRepository(ApplicationDbContext dbContext) : IBaseRepository<PossibleRemoteAreaEntity>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task CreateAsync(PossibleRemoteAreaEntity item)
    {
        await _dbContext.PossibleRemoteAreas.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PossibleRemoteAreaEntity> UpdateAsync(PossibleRemoteAreaEntity item)
    {
        _dbContext.PossibleRemoteAreas.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(PossibleRemoteAreaEntity item)
    {
        _dbContext.PossibleRemoteAreas.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PossibleRemoteAreaEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.PossibleRemoteAreas.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<PossibleRemoteAreaEntity>> GetAllAsync()
    {
        var list = await _dbContext.PossibleRemoteAreas.ToListAsync();

        return list;
    }
}