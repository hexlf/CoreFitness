using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class MembershipsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}