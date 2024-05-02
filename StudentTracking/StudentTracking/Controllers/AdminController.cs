using Microsoft.AspNetCore.Mvc;
using StudentTracking.Domain.Entities.Main;
using StudentTracking.Models;
using StudentTracking.Service.Interfaces.Main;

namespace StudentTracking.Controllers;

public class AdminController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    // GET
    public async Task<IActionResult> Index()
    {
        try
        {
            IEnumerable<UserEntity> users = await _userService.GetAllAsync();
            
            return View("Index", users);
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
}