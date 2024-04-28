namespace StudentTracking.Domain.Entities.Contract;

public class ContractEntity : BaseEntity
{
    public string EducationProfile { get; set; }
    public string Agency { get; set; }
    public string Status { get; set; }
    public string SpecialtyCode { get; set; }
    public string Number { get; set; }
    public string Qualification { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    public Guid CompanyId { get; set; }
    public Guid FacultyId { get; set; }
}