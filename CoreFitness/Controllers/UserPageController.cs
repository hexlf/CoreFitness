using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers
{
    public class UserPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
