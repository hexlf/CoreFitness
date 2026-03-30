using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class SignInPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
