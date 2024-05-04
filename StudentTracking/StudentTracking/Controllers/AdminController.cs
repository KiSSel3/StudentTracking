using Microsoft.AspNetCore.Mvc;
using StudentTracking.Domain.Entities.Main;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Main;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

public class AdminController(IUserService userService, IFacultyService facultyService) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IFacultyService _facultyService = facultyService;

    // GET
    public async Task<IActionResult> Index()
    {
        try
        {
            AdminViewModel viewModel = new AdminViewModel();
            viewModel.Users = await _userService.GetAllAsync();

            viewModel.Faculties = await _facultyService.GetFacultyListAsync();
            
            return View("Index", viewModel);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }

    public async Task<IActionResult> UpdateUser(Guid id)
    {
        try
        {
            await _userService.UpdateUserAccess(id);
            
            return Redirect($"/Admin/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            
            return Redirect($"/Admin/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult CreateFaculty()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFaculty(FacultyEntity faculty)
    {
        try
        {
            await _facultyService.CreateFaculty(faculty);
            
            return Redirect($"/Admin/Index/");

        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateFaculty(Guid id)
    {
        try
        {
            var faculty = await _facultyService.GetFacultyById(id);
            if (faculty is null)
            {
                throw new Exception("Faculty not found");
            }

            return View(faculty);

        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateFaculty(FacultyEntity faculty)
    {
        try
        {
            await _facultyService.UpdateFaculty(faculty);
            
            return Redirect($"/Admin/Index/");

        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    public async Task<IActionResult> DeleteFaculty(Guid id)
    {
        try
        {
            await _facultyService.DeleteFacultyAsync(id);
            
            return Redirect($"/Admin/Index/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
}