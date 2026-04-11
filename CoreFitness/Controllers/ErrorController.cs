using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
