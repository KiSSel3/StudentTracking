using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.DataManager.Interfaces.Contract;

public interface IContractRepository : IBaseRepository<ContractEntity>
{
    public Task<IEnumerable<ContractEntity>> GetByFacultyIdAsync(Guid facultyId);
    public Task<IEnumerable<ContractEntity>> GetByCompanyIdAsync(Guid companyId);
}