using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

[Authorize]
public class ContractController(
    IContractService contractService,
    ICompanyService companyService,
    IFacultyService facultyService,
    IContractSortingService sortingService,
    IContractFilteringService filteringService,
    IPossibleSpecialtyService possibleSpecialtyService)
    : Controller
{
    private readonly IContractService _contractService = contractService;
    private readonly ICompanyService _companyService = companyService;
    private readonly IFacultyService _facultyService = facultyService;
    private readonly IContractFilteringService _filteringService = filteringService;
    private readonly IContractSortingService _sortingService = sortingService;
    private readonly IPossibleSpecialtyService _possibleSpecialtyService = possibleSpecialtyService;

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
    
    [HttpPost]
    public async Task<IActionResult> Index(ContractFormViewModel contractFormViewModel)
    {
        try
        {
            ContractViewModel contractViewModel = new ContractViewModel();

            contractViewModel.FullContracts = await _contractService.GetFullContractListAsync();
            if (contractViewModel.FullContracts is null)
            {
                throw new Exception("Ошибка получения договоров");
            }
            
            if (contractFormViewModel.FacultyId is not null)
            {
                contractViewModel.CurrentFaculty = await _facultyService.GetFacultyById(contractFormViewModel.FacultyId.Value);
                
                contractViewModel.FullContracts = _filteringService.ByFaculty(contractFormViewModel.FacultyId.Value, contractViewModel.FullContracts);
            }

            if (!string.IsNullOrEmpty(contractFormViewModel.KeywordSearch))
            {
                contractViewModel.FullContracts = _filteringService.KeywordSearch(contractFormViewModel.KeywordSearch, contractViewModel.FullContracts);
            }

            contractViewModel.FullContracts = _sortingService.SortContracts(contractViewModel.FullContracts,
                contractFormViewModel.SortingParameter, contractFormViewModel.IsSortingDirectionDesc);
            
            await GetSupportingInformation(contractViewModel);
            await UpdateContractViewModel(contractViewModel, contractFormViewModel);
            
            return View("Index", contractViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        try
        {
            UpdateContractViewModel updateContractViewModel = new UpdateContractViewModel();

            var fullContract = await _contractService.GetFullContractListAsync();
            updateContractViewModel.FullContract = fullContract.FirstOrDefault(fc => fc.Contract.Id.Equals(id));
            if (updateContractViewModel.FullContract is null)
            {
                throw new Exception("Contract not found");
            }

            var faculties = await _facultyService.GetFacultyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения факультетов");
            }
            
            var possibleSpecialties = await _possibleSpecialtyService.GetPossibleSpecialtyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения специальностей");
            }
            updateContractViewModel.PossibleSpecialtiesList = possibleSpecialties;

            updateContractViewModel.Faculties = faculties;

            return View("Edit", updateContractViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateContractFormViewModel updateContractFormViewModel)
    {
        try
        {
            await _contractService.UpdateContractAsync(updateContractFormViewModel);
            
            return await Index();
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
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            NewContractViewModel newContractViewModel = new NewContractViewModel();
            
            var faculties = await _facultyService.GetFacultyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения факультетов");
            }

            newContractViewModel.Faculties = faculties;
            
            var possibleSpecialties = await _possibleSpecialtyService.GetPossibleSpecialtyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения специальностей");
            }
            newContractViewModel.PossibleSpecialtiesList = possibleSpecialties;
            
            return View("Create", newContractViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(NewContractFormViewModel newContractFormViewModel)
    {
        try
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(newContractFormViewModel.ShortName))
            {
                ModelState.AddModelError("ShortName", "Необходимо заполнить поле имени");
            }

            if (string.IsNullOrWhiteSpace(newContractFormViewModel.FullName))
            {
                ModelState.AddModelError("FullName", "Необходимо заполнить поле для полного имени");
            }

            if (string.IsNullOrWhiteSpace(newContractFormViewModel.Address))
            {
                ModelState.AddModelError("Address", "Необходимо заполнить поле для адреса");
            }

            if (string.IsNullOrWhiteSpace(newContractFormViewModel.Director))
            {
                ModelState.AddModelError("Director", "Необходимо заполнить поле для директора");
            }
            
            if (!ModelState.IsValid)
            {
                return await Create();
            }
            
            await _contractService.CreateContractAsync(newContractFormViewModel);
            
            return Redirect($"/Contract/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _contractService.DeleteContractAsync(id);

            return Redirect($"/Contract/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    public async Task<IActionResult> DownloadExcelFile()
    {
        try
        {
            var stream = await _contractService.WriteToFile();
                
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            
            return File(stream, contentType, $"Contracts-{DateTime.Now.ToString()}.xlsx");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> ModifyIsHighlight(Guid id, bool value)
    {
        try
        {
            await _contractService.ModifyIsHighlight(id, value);
            
            return Redirect($"/Contract/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }

    
    private async Task UpdateContractViewModel(ContractViewModel contractViewModel, ContractFormViewModel contractFormViewModel)
    {
        contractViewModel.SortingParameter = contractFormViewModel.SortingParameter;
        contractViewModel.IsSortingDirectionDesc = contractFormViewModel.IsSortingDirectionDesc;
        contractViewModel.KeywordSearch = contractFormViewModel.KeywordSearch ?? "";
    }
}