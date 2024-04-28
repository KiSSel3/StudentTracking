using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.DataManager.Implementations.Shared;

public class CompanyRepository(ApplicationDbContext dbContext) : IBaseRepository<CompanyEntity>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task CreateAsync(CompanyEntity item)
    {
        await _dbContext.Companies.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CompanyEntity> UpdateAsync(CompanyEntity item)
    {
        _dbContext.Companies.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(CompanyEntity item)
    {
        _dbContext.Companies.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CompanyEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Companies.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<CompanyEntity>> GetAllAsync()
    {
        var list = await _dbContext.Companies.Where(c => !c.IsDeleted).ToListAsync();

        return list;
    }
}