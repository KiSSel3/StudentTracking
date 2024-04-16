using StudentTracking.Domain.Entities.Contract;

namespace StudentTracking.DataManager.Interfaces.Contract;

public interface IAnnualNumberPeopleRepository : IBaseRepository<AnnualNumberPeople>
{
    public Task<IEnumerable<AnnualNumberPeople>> GetByContactId(Guid contactId);
}