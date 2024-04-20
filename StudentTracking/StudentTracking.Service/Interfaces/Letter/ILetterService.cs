using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Interfaces.Letter;

public interface ILetterService
{
    public Task<IEnumerable<LetterEntity>> GetLetterListAsync();
    public Task<IEnumerable<FullLetterModel>> GetFullLetterListAsync();
}