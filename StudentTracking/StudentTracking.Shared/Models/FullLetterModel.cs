using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Shared.Models;

public class FullLetterModel
{
    public int Number { get; set; }
    public LetterEntity Letter { get; set; }
    public CompanyEntity Company { get; set; }
    public FacultyEntity Faculty { get; set; }
    
    public IEnumerable<StudentEntity> Students { get; set; }
    public IEnumerable<RemoteAreaEntity> RemoteAreas { get; set; }
    public IEnumerable<SpecialtyEntity> Specialties { get; set; }
    public IEnumerable<CountEntity> Counts { get; set; }
}