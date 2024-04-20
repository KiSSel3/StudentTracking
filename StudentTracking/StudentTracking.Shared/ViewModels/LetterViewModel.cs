using StudentTracking.Shared.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Shared.ViewModels;

public class LetterViewModel
{
    public IEnumerable<FullLetterModel> FullLetters { get; set; }
    
    public IEnumerable<FacultyEntity> Faculties { get; set; }
    public FacultyEntity CurrentFaculty { get; set; }
    public SelectList FacultySelectList { get; set; }
    public SelectList CompanySelectList { get; set; }
}