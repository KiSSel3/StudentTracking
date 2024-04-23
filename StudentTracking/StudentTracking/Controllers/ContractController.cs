using Microsoft.AspNetCore.Mvc;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

public class ContractController(
    IContractService contractService,
    ICompanyService companyService,
    IFacultyService facultyService)
    : Controller
{
    private readonly IContractService _contractService = contractService;
    private readonly ICompanyService _companyService = companyService;
    private readonly IFacultyService _facultyService = facultyService;
    /*private readonly ILetterFilteringService _filteringService = filteringService;
    private readonly ILetterSortingService _sortingService = sortingService;*/

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            ContractViewModel contractViewModel = new ContractViewModel();

            contractViewModel.FullContracts = await _contractService.GetFullContractListAsync();
            if (contractViewModel.FullContracts is null)
            {
                throw new Exception("Ошибка получения договоров");
            }

            await GetSupportingInformation(contractViewModel);
            
            return View("Index", contractViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    private async Task GetSupportingInformation(ContractViewModel contractViewModel)
    {
        var faculties = await _facultyService.GetFacultyListAsync();
        if (faculties is null)
        {
            throw new Exception("Ошибка получения факультетов");
        }

        contractViewModel.Faculties = faculties;
    }
    private async Task UpdateContractViewModel(ContractViewModel contractViewModel, ContractFormViewModel contractFormViewModel)
    {
        contractViewModel.SortingParameter = contractFormViewModel.SortingParameter;
        contractViewModel.IsSortingDirectionDesc = contractFormViewModel.IsSortingDirectionDesc;
        contractViewModel.KeywordSearch = contractFormViewModel.KeywordSearch ?? "";
    }
}