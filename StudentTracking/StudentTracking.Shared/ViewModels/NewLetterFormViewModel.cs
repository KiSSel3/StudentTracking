namespace StudentTracking.Shared.ViewModels;

public class NewLetterFormViewModel
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Position { get; set; }
    public string AccommodationProvision { get; set; }
    public string Note { get; set; }
    public string Base { get; set; }
    
    public DateOnly Date { get; set; }
    
    public Guid FacultyId { get; set; }
    public Guid CompanyId { get; set; }
    
    public IEnumerable<string> Students { get; set; }
    public IEnumerable<string> RemoteAreas { get; set; }
    public IEnumerable<string> Specialities { get; set; }
    public IEnumerable<int> Counts { get; set; }
}