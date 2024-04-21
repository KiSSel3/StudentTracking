using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Interfaces.Letter;

public interface ILetterFilteringService
{
    public IEnumerable<FullLetterModel> ByFaculty(Guid facultyId, IEnumerable<FullLetterModel> letters);
    public IEnumerable<FullLetterModel> KeywordSearch(string keyword, IEnumerable<FullLetterModel> letters);
}