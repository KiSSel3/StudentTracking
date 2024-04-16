namespace StudentTracking.Domain.Entities.Letter;

public class RemoteAreaEntity : BaseEntity
{
    public string Value { get; set; }
    
    public Guid LetterId { get; set; }
}