using StudentTracking.Domain.Enums;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Interfaces.Contract;

public interface IContractSortingService
{
    public IEnumerable<FullContractModel> SortContracts(IEnumerable<FullContractModel> contracts,
        ContractSortingParameters sortingParameter, bool isSortingDirectionDesc);
}