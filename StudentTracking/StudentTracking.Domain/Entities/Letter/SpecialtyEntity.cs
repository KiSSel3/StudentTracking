namespace StudentTracking.Domain.Entities.Letter;

public class SpecialtyEntity : BaseEntity
{
    public string Value { get; set; }
    
    public Guid LetterId { get; set; }
}