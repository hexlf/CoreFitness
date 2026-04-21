using Application.UseCases.RegisterUser;
using CoreFitness.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class SetPasswordController : Controller
{
    private readonly RegisterUserHandler _registerHandler;

    public SetPasswordController(RegisterUserHandler registerHandler)
        => _registerHandler = registerHandler;

    public IActionResult Index()
    {
        var email = TempData.Peek("SignUpEmail") as string;
        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Index", "SignUp");

        return View(new SetPasswordViewModel { Email = email });
    }

    [HttpPost]
    public async Task<IActionResult> Register(SetPasswordViewModel model)
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

        TempData.Remove("SignUpEmail");
        return RedirectToAction("Index", "SignInPage");
    }
}