using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
