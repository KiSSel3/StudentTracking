using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.Domain.Entities.Contract;

namespace StudentTracking.DataManager.Implementations.Contract;

public class AnnualNumberPeopleRepository(ApplicationDbContext dbContext) : IAnnualNumberPeopleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task CreateAsync(AnnualNumberPeople item)
    {
        await _dbContext.AnnualNumberPeoples.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AnnualNumberPeople> UpdateAsync(AnnualNumberPeople item)
    {
        _dbContext.AnnualNumberPeoples.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(AnnualNumberPeople item)
    {
        _dbContext.AnnualNumberPeoples.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AnnualNumberPeople> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.AnnualNumberPeoples.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<AnnualNumberPeople>> GetAllAsync()
    {
        var list = await _dbContext.AnnualNumberPeoples.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<AnnualNumberPeople>> GetByContactId(Guid contactId)
    {
        var list = await _dbContext.AnnualNumberPeoples.Where(anp => anp.ContactId == contactId).ToListAsync();
        
        return list;
    }
}