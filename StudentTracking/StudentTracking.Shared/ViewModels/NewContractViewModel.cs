using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Shared.ViewModels;

public class NewContractViewModel
{
    public IEnumerable<FacultyEntity> Faculties { get; set; }
}