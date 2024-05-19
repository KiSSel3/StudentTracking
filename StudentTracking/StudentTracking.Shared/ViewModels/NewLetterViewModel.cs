using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Shared.ViewModels;

public class NewLetterViewModel
{
    public IEnumerable<FacultyEntity> Faculties { get; set; }
    public IEnumerable<CompanyEntity> Companies { get; set; }
    public IEnumerable<PossibleRemoteAreaEntity> PossibleRemoteAreasList { get; set; }
    public IEnumerable<PossibleSpecialtyEntity> PossibleSpecialtiesList { get; set; }
}