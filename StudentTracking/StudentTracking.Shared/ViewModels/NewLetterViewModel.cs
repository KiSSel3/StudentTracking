using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentTracking.Shared.ViewModels;

public class NewLetterViewModel
{
    public SelectList FacultySelectList { get; set; }
    public SelectList CompanySelectList { get; set; }
}