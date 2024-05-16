using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Shared.Models;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Service.Interfaces.Contract;

public interface IContractService
{
    public Task<IEnumerable<ContractEntity>> GetContractListAsync();
    public Task<IEnumerable<FullContractModel>> GetFullContractListAsync();

    public Task UpdateContractAsync(UpdateContractFormViewModel updateContractFormViewModel);
    public Task CreateContractAsync(NewContractFormViewModel newContractFormViewModel);
    public Task DeleteContractAsync(Guid id);
    public Task ModifyIsHighlight(Guid id, bool value);
    public Task<Stream> WriteToFile();
}