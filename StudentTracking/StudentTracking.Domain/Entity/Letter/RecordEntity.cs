namespace StudentTracking.Domain.Entity.Letter;

public class RecordEntity : BaseEntity
{
    public string CompanyName { get; set; }
    public string Adress { get; set; }
    public string LetterNumber { get; set; }
    public DateOnly Date { get; set; }
    public string Position { get; set; }
    public string AccommodationProvision { get; set; }
    public string Note { get; set; }
    public string Base { get; set; }
    
    public Guid FacultyId { get; set; }
}