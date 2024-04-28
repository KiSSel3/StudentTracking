using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.DataManager.Implementations.Shared;

public class FacultyRepository(ApplicationDbContext dbContext) : IBaseRepository<FacultyEntity>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(FacultyEntity item)
    {
        await _dbContext.Faculties.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FacultyEntity> UpdateAsync(FacultyEntity item)
    {
        _dbContext.Faculties.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(FacultyEntity item)
    {
        _dbContext.Faculties.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FacultyEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Faculties.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<FacultyEntity>> GetAllAsync()
    {
        var list = await _dbContext.Faculties.Where(f=>!f.IsDeleted).ToListAsync();

        return list;
    }
}