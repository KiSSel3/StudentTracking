using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.Mappers;

public class ContractToFullContractMapper(
    IAnnualNumberPeopleRepository annualNumberPeopleRepository,
    IBaseRepository<FacultyEntity> facultyRepository,
    IBaseRepository<CompanyEntity> companyRepository)
{
    private readonly IAnnualNumberPeopleRepository _annualNumberPeopleRepository = annualNumberPeopleRepository;
    private readonly IBaseRepository<CompanyEntity> _companyRepository = companyRepository;
    private readonly IBaseRepository<FacultyEntity> _facultyRepository = facultyRepository;


    public async Task<FullContractModel> MapTo(ContractEntity contract, int number)
    {
        FullContractModel fullContract = new FullContractModel();

        fullContract.Number = number;
        
        fullContract.Contract = contract;

        fullContract.Company = await _companyRepository.GetByIdAsync(contract.CompanyId);
        
        fullContract.Faculty = await _facultyRepository.GetByIdAsync(contract.FacultyId);

        fullContract.AnnualNumberPeoples = await _annualNumberPeopleRepository.GetByContactId(contract.Id);

        return fullContract;
    }
}