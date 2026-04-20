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
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var result = await _registerHandler.HandleAsync(
            new RegisterUserCommand(model.Email, model.Password, model.FirstName, model.LastName));

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Error!);
            return View("Index", model);
        }

        return RedirectToAction("Index", "SignInPage");
    }
}