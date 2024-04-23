using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Implementations.Letter;
using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Implementations.Contract;

public class ContractService(
    IAnnualNumberPeopleRepository annualNumberPeopleRepository,
    IContractRepository contractRepository,
    IBaseRepository<CompanyEntity> companyRepository,
    IBaseRepository<FacultyEntity> facultyRepository,
    ILogger<LetterService> logger)
    : IContractService
{
    private readonly IContractRepository _contractRepository = contractRepository;
    private readonly IBaseRepository<CompanyEntity> _companyRepository = companyRepository;
    private readonly IBaseRepository<FacultyEntity> _facultyRepository = facultyRepository;
    private readonly IAnnualNumberPeopleRepository _annualNumberPeopleRepository = annualNumberPeopleRepository;

    private readonly ILogger<LetterService> _logger = logger;

    private void HandleError(string message)
    {
        _logger.LogError(message);
        throw new Exception(message);
    }
    
    public async Task<IEnumerable<ContractEntity>> GetContractListAsync()
    {
        var contracts = await _contractRepository.GetAllAsync();
        
        return contracts;
    }

    public async Task<IEnumerable<FullContractModel>> GetFullContractListAsync()
    {
        var contracts = await _contractRepository.GetAllAsync();

        var fullContracts = contracts.Select((contract, index) =>
        {
            var fullContractModel = new FullContractModel
            {
                Number = index + 1,

                Contract = contract,

                AnnualNumberPeoples = _annualNumberPeopleRepository.GetByContactId(contract.Id).Result,

                Company = _companyRepository.GetByIdAsync(contract.CompanyId).Result,
                Faculty = _facultyRepository.GetByIdAsync(contract.FacultyId).Result
            };

            return fullContractModel;
        });

        return fullContracts;
    }

    public async Task DeleteContractAsync(Guid id)
    {
        var currentContract = await _contractRepository.GetByIdAsync(id);
        if (currentContract is null)
        {
            throw new Exception("Contract not found");
        }

        var annualNumberPeoples = await _annualNumberPeopleRepository.GetByContactId(id);
        foreach (var item in annualNumberPeoples)
        {
            await _annualNumberPeopleRepository.DeleteAsync(item);
        }

        await _contractRepository.DeleteAsync(currentContract);
    }
}