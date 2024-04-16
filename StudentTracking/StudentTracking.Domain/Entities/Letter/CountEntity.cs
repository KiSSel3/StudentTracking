namespace StudentTracking.Domain.Entities.Letter;

public class CountEntity : BaseEntity
{
    public int Value { get; set; }
    
    public Guid LetterId { get; set; }
}