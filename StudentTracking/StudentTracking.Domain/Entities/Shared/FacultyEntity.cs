namespace StudentTracking.Domain.Entities.Shared;

public class FacultyEntity : BaseEntity
{
    public string FullName { get; set; }
    public string Abbreviation { get; set; }
    public bool IsDeleted { get; set; }
}