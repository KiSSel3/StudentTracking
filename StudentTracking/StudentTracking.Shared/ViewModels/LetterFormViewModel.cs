using StudentTracking.Domain.Enums;

namespace StudentTracking.Shared.ViewModels;

public class LetterFormViewModel
{
    public Guid? FacultyId { get; set; }
    public LetterSortingParameters SortingParameter { get; set; }
    public bool IsSortingDirectionDesc { get; set; }
    public string? KeywordSearch { get; set; }
}