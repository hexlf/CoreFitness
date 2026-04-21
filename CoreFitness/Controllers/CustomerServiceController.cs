using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class CustomerServiceController : Controller
{
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult SendMessage(string firstName, string lastName, string email, string? phoneNumber, string message)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
        {
            ModelState.AddModelError("", "Please fill in all required fields.");
            return View("Index");
        }

        TempData["MessageSent"] = "Thank you! Your message has been sent. We will get back to you shortly.";
        return RedirectToAction("Index");
    }
}