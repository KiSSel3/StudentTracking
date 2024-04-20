using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

public class LetterController(ILetterService letterService, IFacultyService facultyService, ICompanyService companyService) : Controller
{
    private readonly ILetterService _letterService = letterService;
    private readonly ICompanyService _companyService = companyService;
    private readonly IFacultyService _facultyService = facultyService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            LetterViewModel letterViewModel = new LetterViewModel();
            
            letterViewModel.FullLetters = await _letterService.GetFullLetterListAsync();
            if (letterViewModel.FullLetters is null)
            {
                throw new Exception("Ошибка получения писем");
            }
            
            return await ShowView(letterViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    

    [HttpPost]
    public async Task<IActionResult> Index(Guid? facultyId)
    {
        try
        {
            LetterViewModel letterViewModel = new LetterViewModel();
            
            letterViewModel.FullLetters = await _letterService.GetFullLetterListAsync();
            if (letterViewModel.FullLetters is null)
            {
                throw new Exception("Ошибка получения писем");
            }

            if (facultyId is not null)
            {
                SortedByFaculty(letterViewModel, facultyId.Value);
            }
            
            return await ShowView(letterViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }

    //TODO: переделать в сервис
    private async Task SortedByFaculty(LetterViewModel letterViewModel, Guid facultyId)
    {
        
        
        //letterViewModel.CurrentFaculty = await _facultyService.;

        letterViewModel.FullLetters = letterViewModel.FullLetters.Where(item => item.Faculty.Equals(facultyId));
    }
    
    private async Task<IActionResult> ShowView(LetterViewModel letterViewModel)
    {
        try
        {
            var companies = await _companyService.GetCompanyListAsync();
            if (companies is null)
            {
                throw new Exception("Ошибка получения компаний");
            }
            letterViewModel.CompanySelectList = new SelectList(companies.ToList(), "Id", "ShortName");
            
            var faculties = await _facultyService.GetFacultyListAsync();
            if (faculties is null)
            {
                throw new Exception("Ошибка получения факультетов");
            }
            letterViewModel.FacultySelectList = new SelectList(faculties.ToList(), "Id", "FullName");
            letterViewModel.Faculties = faculties;
            
            return View("Index", letterViewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
}