using Application.UseCases.SignIn;
using CoreFitness.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreFitness.Controllers;

public class SignInPageController : Controller
{
    private readonly SignInHandler _signInHandler;

    public SignInPageController(SignInHandler signInHandler)
        => _signInHandler = signInHandler;

    public IActionResult Index() => View(new SignInViewModel());

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var result = await _signInHandler.HandleAsync(new SignInCommand(model.Email, model.Password));

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Error!);
            return View("Index", model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, result.UserId!.Value.ToString()),
            new(ClaimTypes.Email, result.Email!),
            new(ClaimTypes.GivenName, result.FirstName!),
            new(ClaimTypes.Surname, result.LastName!),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "UserPage");
    }

    public new async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}