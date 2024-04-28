using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Interfaces.Contract;

public interface IContractFilteringService
{
    public IEnumerable<FullContractModel> ByFaculty(Guid facultyId, IEnumerable<FullContractModel> contracts);
    public IEnumerable<FullContractModel> KeywordSearch(string keyword, IEnumerable<FullContractModel> contracts);
}