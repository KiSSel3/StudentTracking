using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Implementations.Letter;

public class CountRepository(ApplicationDbContext dbContext) : ICountRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(CountEntity item)
    {
        await _dbContext.Counts.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CountEntity> UpdateAsync(CountEntity item)
    {
        _dbContext.Counts.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(CountEntity item)
    {
        _dbContext.Counts.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CountEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Counts.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<CountEntity>> GetAllAsync()
    {
        var list = await _dbContext.Counts.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<CountEntity>> GetByLetterIdAsync(Guid letterId)
    {
        var list = await _dbContext.Counts.Where(c => c.LetterId == letterId).ToListAsync();
        
        return list;
    }
}