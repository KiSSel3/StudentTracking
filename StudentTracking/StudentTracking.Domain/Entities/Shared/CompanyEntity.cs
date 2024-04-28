namespace StudentTracking.Domain.Entities.Shared;

public class CompanyEntity : BaseEntity
{
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Director { get; set; }
    public bool IsDeleted { get; set; }
}