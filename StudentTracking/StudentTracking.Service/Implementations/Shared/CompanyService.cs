using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Shared;

namespace StudentTracking.Service.Implementations.Shared;

public class CompanyService(IBaseRepository<CompanyEntity> companyRepository, ILogger<CompanyService> logger) : ICompanyService
{
    private readonly IBaseRepository<CompanyEntity> _companyRepository = companyRepository;
    private readonly ILogger<CompanyService> _logger = logger;
    
    private void HandleError(string message)
    {
        _logger.LogError(message);
        
        throw new Exception(message);
    }
    
    public async Task<IEnumerable<CompanyEntity>> GetCompanyListAsync()
    {
        var companies = await _companyRepository.GetAllAsync();

        return companies;
    }

    public async Task DeleteCompanyAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company is null)
        {
            HandleError($"Company with id: {id} not found");
        }

        await _companyRepository.DeleteAsync(company);
    }
}