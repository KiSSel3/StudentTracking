using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Interfaces.Contract;

public interface IContractService
{
    public Task<IEnumerable<ContractEntity>> GetContractListAsync();
    public Task<IEnumerable<FullContractModel>> GetFullContractListAsync();

    //public Task UpdateLetterAsync(UpdateLetterFormViewModel updateLetterFormViewModel);
    //public Task CreateLetterAsync(NewLetterFormViewModel newLetterFormViewModel);
    public Task DeleteContractAsync(Guid id);
}