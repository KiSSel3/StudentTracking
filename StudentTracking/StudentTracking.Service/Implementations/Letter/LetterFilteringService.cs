using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Implementations.Letter;

public class LetterFilteringService : ILetterFilteringService
{
    public IEnumerable<FullLetterModel> ByFaculty(Guid facultyId, IEnumerable<FullLetterModel> letters)
    {
        return letters.Where(letter => letter.Letter.FacultyId == facultyId);
    }

    public IEnumerable<FullLetterModel> KeywordSearch(string keyword, IEnumerable<FullLetterModel> letters)
    {
        keyword = keyword.ToLower();
    
        return letters.Where(letter =>
            letter.Faculty.Abbreviation.ToLower().Contains(keyword) ||
            letter.Specialties.Any(specialty => specialty.Value.ToLower().Contains(keyword)) ||
            letter.Company.ShortName.ToLower().Contains(keyword) ||
            letter.Company.Address.ToLower().Contains(keyword) ||
            letter.RemoteAreas.Any(remoteArea => remoteArea.Value.ToLower().Contains(keyword)) ||
            letter.Letter.Base.ToLower().Contains(keyword) ||
            letter.Letter.Number.ToLower().Contains(keyword) ||
            letter.Letter.Date.ToString().Contains(keyword) ||
            letter.Counts.Any(count => count.Value.ToString().ToLower().Contains(keyword)) ||
            letter.Letter.Position.ToLower().Contains(keyword) ||
            letter.Letter.AccommodationProvision.ToLower().Contains(keyword) ||
            letter.Students.Any(student => student.Name.ToLower().Contains(keyword)) ||
            letter.Letter.Note.ToLower().Contains(keyword)
        );
    }

}