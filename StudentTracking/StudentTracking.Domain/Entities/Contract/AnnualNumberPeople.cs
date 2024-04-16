namespace StudentTracking.Domain.Entities.Contract;

public class AnnualNumberPeople : BaseEntity
{
    public int Count { get; set; }
    
    public DateOnly Year { get; set; }
    
    public Guid ContactId { get; set; }
}