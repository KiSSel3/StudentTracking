using StudentTracking.Domain.Enums;

namespace StudentTracking.Shared.ViewModels;

public class ContractFormViewModel
{
    public Guid? FacultyId { get; set; }
    public ContractSortingParameters SortingParameter { get; set; }
    public bool IsSortingDirectionDesc { get; set; }
    public string? KeywordSearch { get; set; }
}