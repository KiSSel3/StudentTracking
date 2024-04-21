using StudentTracking.Shared.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Domain.Enums;

namespace StudentTracking.Shared.ViewModels;

public class LetterViewModel
{
    public IEnumerable<FullLetterModel> FullLetters { get; set; }
    public IEnumerable<FacultyEntity> Faculties { get; set; }
    public FacultyEntity CurrentFaculty { get; set; } = null;

    public LetterSortingParameters SortingParameter { get; set; } = LetterSortingParameters.Number;

    public bool IsSortingDirectionDesc { get; set; } = true;
    public string KeywordSearch { get; set; } = "";
}