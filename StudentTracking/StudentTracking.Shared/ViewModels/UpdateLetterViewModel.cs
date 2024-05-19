using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.ViewModels;

public class UpdateLetterViewModel
{
    public FullLetterModel FullLetter { get; set; }
    
    public IEnumerable<FacultyEntity> Faculties { get; set; }
    public IEnumerable<CompanyEntity> Companies { get; set; }
    public IEnumerable<PossibleRemoteAreaEntity> PossibleRemoteAreasList { get; set; }
    public IEnumerable<PossibleSpecialtyEntity> PossibleSpecialtiesList { get; set; }
    
    public string PossibleRemoteAreas { get; set; }
    public string PossibleSpecialties { get; set; }
}