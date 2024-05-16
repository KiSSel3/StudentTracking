using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Implementations.Letter;
using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Shared.Models;
using StudentTracking.Shared.ViewModels;

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

    public async Task UpdateContractAsync(UpdateContractFormViewModel updateContractFormViewModel)
    {
        var currentContract = await _contractRepository.GetByIdAsync(updateContractFormViewModel.Id);
        if (currentContract is null)
        {
            HandleError("Contract not found");
        }
        
        currentContract.EducationProfile = updateContractFormViewModel.EducationProfile ?? "";
        currentContract.Agency = updateContractFormViewModel.Agency ?? "";
        currentContract.Status = updateContractFormViewModel.Status ?? "";
        currentContract.SpecialtyCode = updateContractFormViewModel.SpecialtyCode ?? "";
        currentContract.Number = updateContractFormViewModel.Number ?? "";
        currentContract.Qualification = updateContractFormViewModel.Qualification ?? "";
        currentContract.StartDate = updateContractFormViewModel.StartDate;
        currentContract.EndDate = updateContractFormViewModel.EndDate;
        currentContract.FacultyId = updateContractFormViewModel.FacultyId;

        await _contractRepository.UpdateAsync(currentContract);

        await UpadateCompanyAsync(updateContractFormViewModel);
        
        var annualNumberPeoples = await _annualNumberPeopleRepository.GetByContactId(updateContractFormViewModel.Id);
        foreach (var item in annualNumberPeoples)
        {
            await _annualNumberPeopleRepository.DeleteAsync(item);
        }
        if (updateContractFormViewModel.Counts is not null && updateContractFormViewModel.Years is not null)
        {
            var counts = updateContractFormViewModel.Counts.ToList();
            var years = updateContractFormViewModel.Years.ToList();

            if (counts.Count == years.Count)
            {
                for (int i = 0; i < counts.Count; i++)
                {
                    await _annualNumberPeopleRepository.CreateAsync(new AnnualNumberPeople()
                        { Year = years[i], Count = counts[i], ContactId = currentContract.Id });
                }
            }
        }
    }

    private async Task UpadateCompanyAsync(UpdateContractFormViewModel updateContractFormViewModel)
    {
        var currentCompany = await _companyRepository.GetByIdAsync(updateContractFormViewModel.CompanyId);
        if (currentCompany is null)
        {
            HandleError("Company not found");
        }

        currentCompany.Address = updateContractFormViewModel.Address;
        currentCompany.ShortName = updateContractFormViewModel.ShortName;
        currentCompany.FullName = updateContractFormViewModel.FullName;
        currentCompany.Director = updateContractFormViewModel.Director;

        await _companyRepository.UpdateAsync(currentCompany);
    }
    

    public async Task CreateContractAsync(NewContractFormViewModel newContractFormViewModel)
    {
        var newCompany = new CompanyEntity()
        {
            Address = newContractFormViewModel.Address,
            ShortName = newContractFormViewModel.ShortName,
            FullName = newContractFormViewModel.FullName,
            Director = newContractFormViewModel.Director,
            IsDeleted = false
        };
        await _companyRepository.CreateAsync(newCompany);
        
        var newContract = new ContractEntity()
        {
            EducationProfile = newContractFormViewModel.EducationProfile  ?? "",
            Agency = newContractFormViewModel.Agency ?? "",
            Status = newContractFormViewModel.Status ?? "",
            SpecialtyCode = newContractFormViewModel.SpecialtyCode ?? "",
            Number = newContractFormViewModel.Number ?? "",
            Qualification = newContractFormViewModel.Qualification ?? "",
            StartDate = newContractFormViewModel.StartDate,
            EndDate = newContractFormViewModel.EndDate,
            FacultyId = newContractFormViewModel.FacultyId,
            CompanyId = newCompany.Id,
            IsHighlight = false
        };
        await _contractRepository.CreateAsync(newContract);
        
        if (newContractFormViewModel.Counts is not null && newContractFormViewModel.Years is not null)
        {
            var counts = newContractFormViewModel.Counts.ToList();
            var years = newContractFormViewModel.Years.ToList();

            if (counts.Count == years.Count)
            {
                for (int i = 0; i < counts.Count; i++)
                {
                    await _annualNumberPeopleRepository.CreateAsync(new AnnualNumberPeople()
                        { Year = years[i], Count = counts[i], ContactId = newContract.Id });
                }
            }
        }
    }

    public async Task DeleteContractAsync(Guid id)
    {
        var currentContract = await _contractRepository.GetByIdAsync(id);
        if (currentContract is null)
        {
            HandleError("Contract not found");
        }

        var annualNumberPeoples = await _annualNumberPeopleRepository.GetByContactId(id);
        foreach (var item in annualNumberPeoples)
        {
            await _annualNumberPeopleRepository.DeleteAsync(item);
        }

        await _contractRepository.DeleteAsync(currentContract);

        var currentCompany = await _companyRepository.GetByIdAsync(currentContract.CompanyId);
        if (currentCompany is null)
        {
            HandleError("Company not found");
        }

        currentCompany.IsDeleted = true;

        await _companyRepository.UpdateAsync(currentCompany);
    }

    public async Task ModifyIsHighlight(Guid id, bool value)
    {
        var currentContract = await _contractRepository.GetByIdAsync(id);
        if (currentContract is null)
        {
            HandleError("Contract not found");
        }

        currentContract.IsHighlight = value;

        await _contractRepository.UpdateAsync(currentContract);
    }

    public async Task<Stream> WriteToFile()
    {
        var fullContracts = await GetFullContractListAsync();

        using (var package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Contracts");
            
            worksheet.Cells[1, 1].Value = "Факультет";
            worksheet.Cells[1, 2].Value = "Имя";
            worksheet.Cells[1, 3].Value = "Адрес";
            worksheet.Cells[1, 4].Value = "Полное имя";
            worksheet.Cells[1, 5].Value = "Профиль";
            worksheet.Cells[1, 6].Value = "Директор";
            worksheet.Cells[1, 7].Value = "Ведомство";
            worksheet.Cells[1, 8].Value = "Номер";
            worksheet.Cells[1, 9].Value = "Статус";
            worksheet.Cells[1, 10].Value = "Дата начала";
            worksheet.Cells[1, 11].Value = "Дата окончания";
            worksheet.Cells[1, 12].Value = "Код специальности";
            worksheet.Cells[1, 13].Value = "Количество";

            int row = 2;
            foreach (var fullContract in fullContracts)
            {
                worksheet.Cells[row, 1].Value = fullContract.Faculty.Abbreviation;
                worksheet.Cells[row, 2].Value = fullContract.Company.ShortName;
                worksheet.Cells[row, 3].Value = fullContract.Company.Address;
                worksheet.Cells[row, 4].Value = fullContract.Company.FullName;
                worksheet.Cells[row, 5].Value = fullContract.Contract.EducationProfile;
                worksheet.Cells[row, 6].Value = fullContract.Company.Director;
                worksheet.Cells[row, 7].Value = fullContract.Contract.Agency;
                worksheet.Cells[row, 8].Value = fullContract.Contract.Number;
                worksheet.Cells[row, 9].Value = fullContract.Contract.Status;
                worksheet.Cells[row, 10].Value = fullContract.Contract.StartDate;
                worksheet.Cells[row, 11].Value = fullContract.Contract.EndDate;
                worksheet.Cells[row, 12].Value = fullContract.Contract.SpecialtyCode;
                
                string counts = string.Join("\n", fullContract.AnnualNumberPeoples
                    .Select(a => $"{a.Year.Year}: {a.Count}"));
                worksheet.Cells[row, 13].Value = counts;

                ++row;
            }
            
            worksheet.Cells.AutoFitColumns();
            
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            
            stream.Position = 0;

            return stream;
        }
    }
}