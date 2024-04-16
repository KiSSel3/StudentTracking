namespace StudentTracking.Domain.Entities.Letter;

public class LetterEntity : BaseEntity
{
    public string Number { get; set; }
    public string Position { get; set; }
    public string AccommodationProvision { get; set; }
    public string Note { get; set; }
    public string Base { get; set; }
    
    public DateOnly Date { get; set; }
    
    public Guid FacultyId { get; set; }
    public Guid CompanyId { get; set; }
}