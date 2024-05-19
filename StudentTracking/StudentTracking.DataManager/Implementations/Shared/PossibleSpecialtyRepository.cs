using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.DataManager.Implementations.Shared;

public class PossibleSpecialtyRepository(ApplicationDbContext dbContext) : IBaseRepository<PossibleSpecialtyEntity>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task CreateAsync(PossibleSpecialtyEntity item)
    {
        await _dbContext.PossibleSpecialties.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PossibleSpecialtyEntity> UpdateAsync(PossibleSpecialtyEntity item)
    {
        _dbContext.PossibleSpecialties.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(PossibleSpecialtyEntity item)
    {
        _dbContext.PossibleSpecialties.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PossibleSpecialtyEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.PossibleSpecialties.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<PossibleSpecialtyEntity>> GetAllAsync()
    {
        var list = await _dbContext.PossibleSpecialties.ToListAsync();

        return list;
    }
}