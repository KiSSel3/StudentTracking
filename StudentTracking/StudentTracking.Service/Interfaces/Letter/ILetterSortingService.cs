using StudentTracking.Domain.Enums;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Interfaces.Letter;

public interface ILetterSortingService
{
    public IEnumerable<FullLetterModel> SortLetters(IEnumerable<FullLetterModel> letters,
        LetterSortingParameters sortingParameter, bool isSortingDirectionDesc);
}