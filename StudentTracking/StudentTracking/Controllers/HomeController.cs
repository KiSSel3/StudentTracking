using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

[Authorize]
public class HomeController(
    ILogger<HomeController> logger,
    IContractService contractService,
    ILetterService letterService,
    ICompanyService companyService,
    IFacultyService facultyService)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly IContractService _contractService = contractService;
    private readonly ILetterService _letterService = letterService;
    private readonly ICompanyService _companyService = companyService;
    private readonly IFacultyService _facultyService = facultyService;

    public async Task<IActionResult> Index()
    {
        try
        {
            var fullLetters = await _letterService.GetFullLetterListAsync();
            var fullContracts = await _contractService.GetFullContractListAsync();
            var companyList = await _companyService.GetCompanyListAsync();
            var facultyList = await _facultyService.GetFacultyListAsync();

            StatisticViewModel viewModel = new StatisticViewModel();
            viewModel.LetterCount = fullLetters.ToList().Count;
            viewModel.ContractCount = fullContracts.ToList().Count;
            viewModel.CompanyCount = companyList.ToList().Count;
            viewModel.FacultyCount = facultyList.ToList().Count;
            
            return View(viewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
}