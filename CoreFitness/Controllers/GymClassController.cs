using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers
{
    public class GymClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
