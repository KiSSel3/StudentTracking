using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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
    
    public async Task<IActionResult> DownloadExcelFile()
    {
        try
        {
            var fullLetters = await _letterService.GetFullLetterListAsync();
            var fullContracts = await _contractService.GetFullContractListAsync();
            var companyList = await _companyService.GetCompanyListAsync();
            var facultyList = await _facultyService.GetFacultyListAsync();

            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Letters");

                worksheet.Cells[1, 1].Value = "Факультеты:";
                worksheet.Cells[1, 2].Value = facultyList.ToList().Count;
                
                worksheet.Cells[2, 1].Value = "Компании:";
                worksheet.Cells[2, 2].Value = companyList.ToList().Count;
                
                worksheet.Cells[3, 1].Value = "Письма:";
                worksheet.Cells[3, 2].Value = fullLetters.ToList().Count;
                
                worksheet.Cells[4, 1].Value = "Договоры:";
                worksheet.Cells[4, 2].Value = fullContracts.ToList().Count;

                worksheet.Cells.AutoFitColumns();

                MemoryStream stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;


                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, $"Statistic-{DateTime.Now.ToString()}.xlsx");
            }
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
}