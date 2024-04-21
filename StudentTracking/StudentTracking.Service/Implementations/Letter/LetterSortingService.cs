using StudentTracking.Domain.Enums;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Implementations.Letter;

public class LetterSortingService : ILetterSortingService
{
    public IEnumerable<FullLetterModel> SortLetters(IEnumerable<FullLetterModel> letters, LetterSortingParameters sortingParameter, bool isSortingDirectionDesc)
    {
        switch (sortingParameter)
        {
            case LetterSortingParameters.Number:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Number) :
                    letters.OrderBy(l => l.Number);
            
            case LetterSortingParameters.Faculty:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Faculty.Abbreviation) :
                    letters.OrderBy(l => l.Faculty.Abbreviation);

            case LetterSortingParameters.Specialty:
                return isSortingDirectionDesc ? 
                    letters.OrderByDescending(l => l.Specialties.Any() ?
                        l.Specialties.Min(s => s.Value) : null) :
                    letters.OrderBy(l => l.Specialties.Any() ?
                        l.Specialties.Min(s => s.Value) : null);

            case LetterSortingParameters.CompanyName:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Company.FullName) :
                    letters.OrderBy(l => l.Company.FullName);

            case LetterSortingParameters.CompanyAddress:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Company.Address) :
                    letters.OrderBy(l => l.Company.Address);

            case LetterSortingParameters.RemoteAreas:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.RemoteAreas.Any() ?
                        l.RemoteAreas.Min(r => r.Value) : null) :
                    letters.OrderBy(l => l.RemoteAreas.Any() ? 
                        l.RemoteAreas.Min(r => r.Value) : null);

            case LetterSortingParameters.Base:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Letter.Base) :
                    letters.OrderBy(l => l.Letter.Base);

            case LetterSortingParameters.LetterNumber:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Letter.Number) :
                    letters.OrderBy(l => l.Letter.Number);

            case LetterSortingParameters.Date:
                return isSortingDirectionDesc ? 
                    letters.OrderByDescending(l => l.Letter.Date) : 
                    letters.OrderBy(l => l.Letter.Date);

            case LetterSortingParameters.Counts:
                return isSortingDirectionDesc ? 
                    letters.OrderByDescending(l => l.Counts.Sum(c => c.Value)) : 
                    letters.OrderBy(l => l.Counts.Sum(c => c.Value));

            case LetterSortingParameters.Position:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Letter.Position) :
                    letters.OrderBy(l => l.Letter.Position);

            case LetterSortingParameters.AccommodationProvision:
                return isSortingDirectionDesc ?
                    letters.OrderByDescending(l => l.Letter.AccommodationProvision) :
                    letters.OrderBy(l => l.Letter.AccommodationProvision);

            case LetterSortingParameters.Students:
                return isSortingDirectionDesc ? 
                    letters.OrderByDescending(l => l.Students.Any() ?
                        l.Students.Min(s => s.Name) : null) :
                    letters.OrderBy(l => l.Students.Any() ?
                        l.Students.Min(s => s.Name) : null);

            case LetterSortingParameters.Note:
                return isSortingDirectionDesc ? 
                    letters.OrderByDescending(l => l.Letter.Note) :
                    letters.OrderBy(l => l.Letter.Note);

            default:
                throw new ArgumentException("Invalid sorting parameter", nameof(sortingParameter));
        }
    }
}