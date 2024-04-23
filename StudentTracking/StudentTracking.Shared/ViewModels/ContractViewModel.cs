using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Domain.Enums;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.ViewModels;

public class ContractViewModel
{
    public IEnumerable<FullContractModel> FullContracts { get; set; }
    public IEnumerable<FacultyEntity> Faculties { get; set; }
    public FacultyEntity CurrentFaculty { get; set; } = null;

    public ContractSortingParameters SortingParameter { get; set; } = ContractSortingParameters.Number;

    public bool IsSortingDirectionDesc { get; set; } = true;
    public string KeywordSearch { get; set; } = "";
}