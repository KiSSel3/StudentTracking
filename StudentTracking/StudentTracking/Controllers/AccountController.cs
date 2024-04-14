using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracking.Service.Interfaces.Main;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Controllers;

public class AccountController(IUserService _userService) : Controller
{
    private readonly IUserService _userService = _userService;
    
    [HttpGet]
    public IActionResult Authorization()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Authorization");
        }
        
        try
        {
            var response = await _userService.LoginAsync(model);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response));

            return Redirect("/");
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("AuthorizationError", ex.Message);

            return View("Authorization");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Authorization");
        }
        
        try
        {
            await _userService.RegisterAsync(model);

            //TODO: Добавить сообщение об обращении к админу
            return Redirect("/");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("AuthorizationError", ex.Message);

            return View("Authorization");
        }
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}