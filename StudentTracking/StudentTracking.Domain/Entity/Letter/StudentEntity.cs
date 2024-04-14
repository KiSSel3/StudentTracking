namespace StudentTracking.Domain.Entity.Letter;

public class StudentEntity : BaseEntity
{
    public string Name { get; set; }
    
    public Guid RecordId { get; set; }
}