using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Implementations.Letter;

public class SpecialtyRepository(ApplicationDbContext dbContext) : ISpecialtyRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(SpecialtyEntity item)
    {
        await _dbContext.Specialties.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SpecialtyEntity> UpdateAsync(SpecialtyEntity item)
    {
        _dbContext.Specialties.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(SpecialtyEntity item)
    {
        _dbContext.Specialties.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SpecialtyEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Specialties.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<SpecialtyEntity>> GetAllAsync()
    {
        var list = await _dbContext.Specialties.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<SpecialtyEntity>> GetByLetterIdAsync(Guid letterId)
    {
        var list = await _dbContext.Specialties.Where(s => s.LetterId == letterId).ToListAsync();
        
        return list;
    }
}