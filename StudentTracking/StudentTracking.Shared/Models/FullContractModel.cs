using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Shared.Models;

public class FullContractModel
{
    public ContractEntity Contract { get; set; }
    public CompanyEntity Company { get; set; }
    public FacultyEntity Faculty { get; set; }
    
    public IEnumerable<AnnualNumberPeople> AnnualNumberPeoples { get; set; }
}