using StudentTracking.Domain.Entities.Contract;

namespace StudentTracking.Shared.ViewModels;

public class UpdateContractFormViewModel
{
    public Guid Id { get; set; }
    public string EducationProfile { get; set; }
    public string Agency { get; set; }
    public string Status { get; set; }
    public string SpecialtyCode { get; set; }
    public string Number { get; set; }
    public string Qualification { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    public Guid FacultyId { get; set; }
    public Guid CompanyId { get; set; }
    
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Director { get; set; }
    
    public IEnumerable<DateOnly> Years { get; set; }
    public IEnumerable<int> Counts { get; set; }
}