using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

[Authorize]
public class LetterController(
    ILetterService letterService,
    IFacultyService facultyService,
    ICompanyService companyService,
    ILetterFilteringService filteringService,
    ILetterSortingService sortingService,
    IPossibleSpecialtyService possibleSpecialtyService,
    IPossibleRemoteAreaService possibleRemoteAreaService) : Controller
{
    private readonly ILetterService _letterService = letterService;
    private readonly ICompanyService _companyService = companyService;
    private readonly IFacultyService _facultyService = facultyService;
    private readonly ILetterFilteringService _filteringService = filteringService;
    private readonly ILetterSortingService _sortingService = sortingService;
    private readonly IPossibleRemoteAreaService _possibleRemoteAreaService = possibleRemoteAreaService;
    private readonly IPossibleSpecialtyService _possibleSpecialtyService = possibleSpecialtyService;
    
    [HttpGet]
    public async Task<IActionResult> Index(string checkDescr = "")
    {
        try
        {
            LetterViewModel letterViewModel = new LetterViewModel();

            letterViewModel.CheckDescr = checkDescr;
            
            letterViewModel.FullLetters = await _letterService.GetFullLetterListAsync();
            if (letterViewModel.FullLetters is null)
            {
                throw new Exception("Ошибка получения писем");
            }

            await GetSupportingInformation(letterViewModel);
            
            return View("Index", letterViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    

    [HttpPost]
    public async Task<IActionResult> Index(LetterFormViewModel letterFormViewModel)
    {
        try
        {
            LetterViewModel letterViewModel = new LetterViewModel();
            
            letterViewModel.FullLetters = await _letterService.GetFullLetterListAsync();
            if (letterViewModel.FullLetters is null)
            {
                throw new Exception("Ошибка получения писем");
            }

            if (letterFormViewModel.FacultyId is not null)
            {
                letterViewModel.CurrentFaculty = await _facultyService.GetFacultyById(letterFormViewModel.FacultyId.Value);
                
                letterViewModel.FullLetters = _filteringService.ByFaculty(letterFormViewModel.FacultyId.Value, letterViewModel.FullLetters);
            }

            if (!string.IsNullOrEmpty(letterFormViewModel.KeywordSearch))
            {
                letterViewModel.FullLetters = _filteringService.KeywordSearch(letterFormViewModel.KeywordSearch, letterViewModel.FullLetters);
            }

            letterViewModel.FullLetters = _sortingService.SortLetters(letterViewModel.FullLetters,
                letterFormViewModel.SortingParameter, letterFormViewModel.IsSortingDirectionDesc);

            await GetSupportingInformation(letterViewModel);
            await UpdateLetterViewModel(letterViewModel, letterFormViewModel);
            
            return View("Index", letterViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            NewLetterViewModel newLetterViewModel = new NewLetterViewModel();
            
            var companies = await _companyService.GetCompanyListAsync();
            if (companies is null)
            {
                throw new Exception("Ошибка получения компаний");
            }
            newLetterViewModel.Companies = companies;
            
            var faculties = await _facultyService.GetFacultyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения факультетов");
            }
            newLetterViewModel.Faculties = faculties;
            
            var possibleRemoteAreas = await _possibleRemoteAreaService.GetPossibleRemoteAreaListAsync();
            if (possibleRemoteAreas is null)
            {
                throw new Exception("Ошибка получения отсталых районов");
            }
            newLetterViewModel.PossibleRemoteAreas = JsonConvert.SerializeObject(possibleRemoteAreas);
            
            var possibleSpecialties = await _possibleSpecialtyService.GetPossibleSpecialtyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения специальностей");
            }
            newLetterViewModel.PossibleSpecialties = JsonConvert.SerializeObject(possibleSpecialties);
            
            return View("Create", newLetterViewModel);
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
            await _letterService.DeleteLetterAsync(id);
            
            return Redirect($"/Letter/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(NewLetterFormViewModel newLetterFormViewModel)
    {
        try
        {
            await _letterService.CreateLetterAsync(newLetterFormViewModel);
            
            return Redirect($"/Letter/Index/");
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
            UpdateLetterViewModel updateLetterViewModel = new UpdateLetterViewModel();
            
            var fullLetters = await _letterService.GetFullLetterListAsync();
            updateLetterViewModel.FullLetter = fullLetters.FirstOrDefault(fl => fl.Letter.Id.Equals(id));
            if (updateLetterViewModel.FullLetter is null)
            {
                throw new Exception("Letter not found");
            }
            
            var companies = await _companyService.GetCompanyListAsync();
            if (companies is null)
            {
                throw new Exception("Ошибка получения компаний");
            }

            updateLetterViewModel.Companies = companies;
            
            var faculties = await _facultyService.GetFacultyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения факультетов");
            }

            updateLetterViewModel.Faculties = faculties;
            
            var possibleRemoteAreas = await _possibleRemoteAreaService.GetPossibleRemoteAreaListAsync();
            if (possibleRemoteAreas is null)
            {
                throw new Exception("Ошибка получения отсталых районов");
            }
            updateLetterViewModel.PossibleRemoteAreasList = possibleRemoteAreas;
            updateLetterViewModel.PossibleRemoteAreas = JsonConvert.SerializeObject(possibleRemoteAreas);
            
            var possibleSpecialties = await _possibleSpecialtyService.GetPossibleSpecialtyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения специальностей");
            }
            updateLetterViewModel.PossibleSpecialtiesList = possibleSpecialties;
            updateLetterViewModel.PossibleSpecialties = JsonConvert.SerializeObject(possibleSpecialties);
            
            return View("Edit", updateLetterViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateLetterFormViewModel updateLetterFormViewModel)
    {
        try
        {
            await _letterService.UpdateLetterAsync(updateLetterFormViewModel);
            
            return Redirect($"/Letter/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    public async Task<IActionResult> CheckLetter(Guid id)
    {
        try
        {
            var checkDesc = await _letterService.CheckLetterAsync(id);
            
            return await Index(checkDesc);
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
            var stream = await _letterService.WriteToFile();
                
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            
            return File(stream, contentType, $"Letters-{DateTime.Now.ToString()}.xlsx");
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
            await _letterService.ModifyIsHighlight(id, value);
            
            return Redirect($"/Letter/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    private async Task GetSupportingInformation(LetterViewModel letterViewModel)
    {
        var faculties = await _facultyService.GetFacultyListAsync();
        if (faculties is null)
        {
            throw new Exception("Ошибка получения факультетов");
        }

        letterViewModel.Faculties = faculties;
    }

    private async Task UpdateLetterViewModel(LetterViewModel letterViewModel, LetterFormViewModel letterFormViewModel)
    {
        letterViewModel.SortingParameter = letterFormViewModel.SortingParameter;
        letterViewModel.IsSortingDirectionDesc = letterFormViewModel.IsSortingDirectionDesc;
        letterViewModel.KeywordSearch = letterFormViewModel.KeywordSearch ?? "";
    }
}