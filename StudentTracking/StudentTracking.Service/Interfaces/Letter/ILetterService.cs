using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Shared.Models;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Service.Interfaces.Letter;

public interface ILetterService
{
    public Task<IEnumerable<LetterEntity>> GetLetterListAsync();
    public Task<IEnumerable<FullLetterModel>> GetFullLetterListAsync();

    public Task UpdateLetterAsync(UpdateLetterFormViewModel updateLetterFormViewModel);
    public Task CreateLetterAsync(NewLetterFormViewModel newLetterFormViewModel);
    public Task DeleteLetterAsync(Guid id);
    public Task<string> CheckLetterAsync(Guid id);
    public Task<Stream> WriteToFile();
}