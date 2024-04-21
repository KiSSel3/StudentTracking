using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.ViewModels;

public class UpdateLetterViewModel
{
    public FullLetterModel FullLetter { get; set; }
    
    public SelectList FacultySelectList { get; set; }
    public SelectList CompanySelectList { get; set; }
}