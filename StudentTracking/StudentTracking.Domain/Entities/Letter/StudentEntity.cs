namespace StudentTracking.Domain.Entities.Letter;

public class StudentEntity : BaseEntity
{
    public string Name { get; set; }
    
    public Guid LetterId { get; set; }
}