using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.Domain.Entities.Contract;

namespace StudentTracking.DataManager.Implementations.Contract;

public class ContractRepository(ApplicationDbContext dbContext) : IContractRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task CreateAsync(ContractEntity item)
    {
        await _dbContext.Contracts.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ContractEntity> UpdateAsync(ContractEntity item)
    {
        _dbContext.Contracts.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(ContractEntity item)
    {
        _dbContext.Contracts.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ContractEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Contracts.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<ContractEntity>> GetAllAsync()
    {
        var list = await _dbContext.Contracts.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<ContractEntity>> GetByFacultyIdAsync(Guid facultyId)
    {
        var list = await _dbContext.Contracts.Where(c => c.FacultyId == facultyId).ToListAsync();
        
        return list;
    }

    public async Task<ContractEntity> GetByCompanyIdAsync(Guid companyId)
    {
        var contract = await _dbContext.Contracts.FirstOrDefaultAsync(c => c.CompanyId == companyId);
        
        return contract;
    }
}