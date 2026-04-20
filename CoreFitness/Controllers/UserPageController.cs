using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

[Authorize]
public class UserPageController : Controller
{
    public IActionResult Index() => View();
}