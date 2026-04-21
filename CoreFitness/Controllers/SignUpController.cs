using Application.UseCases.RegisterUser;
using CoreFitness.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class SignUpController : Controller
{
    private readonly RegisterUserHandler _registerHandler;

    public SignUpController(RegisterUserHandler registerHandler)
        => _registerHandler = registerHandler;

    public IActionResult Index() => View(new RegisterViewModel());

    [HttpPost]
    public IActionResult Continue(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        TempData["SignUpEmail"] = model.Email;
        return RedirectToAction("Index", "SetPassword");
    }
}