using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.ViewModels;

public class UpdateContractViewModel
{
    public FullContractModel FullContract { get; set; }
    
    public SelectList FacultySelectList { get; set; }
}