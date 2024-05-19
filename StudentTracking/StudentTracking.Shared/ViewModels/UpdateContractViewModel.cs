using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.ViewModels;

public class UpdateContractViewModel
{
    public FullContractModel FullContract { get; set; }
    
    public IEnumerable<FacultyEntity> Faculties { get; set; }
    public IEnumerable<PossibleSpecialtyEntity> PossibleSpecialtiesList { get; set; }
}