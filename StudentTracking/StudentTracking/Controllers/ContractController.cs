using Microsoft.AspNetCore.Mvc;

namespace StudentTracking.Controllers;

public class ContractController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}