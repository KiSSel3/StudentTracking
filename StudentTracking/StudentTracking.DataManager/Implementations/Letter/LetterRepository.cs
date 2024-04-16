using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Implementations.Letter;

public class LetterRepository(ApplicationDbContext dbContext) : ILetterRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(LetterEntity item)
    {
        await _dbContext.Letters.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<LetterEntity> UpdateAsync(LetterEntity item)
    {
        _dbContext.Letters.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(LetterEntity item)
    {
        _dbContext.Letters.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<LetterEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Letters.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<LetterEntity>> GetAllAsync()
    {
        var list = await _dbContext.Letters.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<LetterEntity>> GetByFacultyIdAsync(Guid facultyId)
    {
        var list = await _dbContext.Letters.Where(l => l.FacultyId == facultyId).ToListAsync();
        
        return list;
    }

    public async Task<IEnumerable<LetterEntity>> GetByCompanyIdAsync(Guid companyId)
    {
        var list = await _dbContext.Letters.Where(l => l.CompanyId == companyId).ToListAsync();
        
        return list;
    }
}