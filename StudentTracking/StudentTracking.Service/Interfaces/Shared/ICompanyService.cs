using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Service.Interfaces.Shared;

public interface ICompanyService
{
    public Task<IEnumerable<CompanyEntity>> GetCompanyListAsync();
    public Task DeleteCompanyAsync(Guid id);
}