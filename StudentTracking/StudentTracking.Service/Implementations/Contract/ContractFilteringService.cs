using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Implementations.Contract;

public class ContractFilteringService : IContractFilteringService
{
    public IEnumerable<FullContractModel> ByFaculty(Guid facultyId, IEnumerable<FullContractModel> contracts)
    {
        return contracts.Where(contract => contract.Contract.FacultyId == facultyId);
    }

    public IEnumerable<FullContractModel> KeywordSearch(string keyword, IEnumerable<FullContractModel> contracts)
    {
        keyword = keyword.ToLower();

        return contracts.Where(contract =>
            contract.Faculty.Abbreviation.ToLower().Contains(keyword) ||
            contract.Company.ShortName.ToLower().Contains(keyword) ||
            contract.Company.Address.ToLower().Contains(keyword) ||
            contract.Company.FullName.ToLower().Contains(keyword) ||
            contract.Contract.EducationProfile.ToLower().Contains(keyword) ||
            contract.Company.Director.ToLower().Contains(keyword) ||
            contract.Contract.Agency.ToLower().Contains(keyword) ||
            contract.Contract.Number.ToLower().Contains(keyword) ||
            contract.Contract.Status.ToLower().Contains(keyword) ||
            contract.Contract.StartDate.ToString().Contains(keyword) ||
            contract.Contract.EndDate.ToString().Contains(keyword) ||
            contract.Contract.SpecialtyCode.ToLower().Contains(keyword) ||
            contract.AnnualNumberPeoples.Any(anp=>$"{anp.Year.Year}г: {anp.Count}".Contains(keyword))
        );
    }
}