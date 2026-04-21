using Application.UseCases.DeleteUser;
using Application.UseCases.UpdateUserProfile;
using CoreFitness.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreFitness.Controllers;

[Authorize]
public class UserPageController : Controller
{
    private readonly UpdateUserProfileHandler _updateHandler;
    private readonly DeleteUserHandler _deleteHandler;

    public UserPageController(UpdateUserProfileHandler updateHandler, DeleteUserHandler deleteHandler)
    {
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public IActionResult Index()
    {
        var model = new UpdateProfileViewModel
        {
            FirstName = User.FindFirstValue(ClaimTypes.GivenName) ?? "",
            LastName = User.FindFirstValue(ClaimTypes.Surname) ?? "",
            Email = User.FindFirstValue(ClaimTypes.Email) ?? "",
            PhoneNumber = User.FindFirstValue("PhoneNumber"),
            ExistingProfileImagePath = User.FindFirstValue("ProfileImagePath"),
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        string? imagePath = model.ExistingProfileImagePath;

        if (model.ProfileImage is { Length: > 0 })
        {
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
            Directory.CreateDirectory(uploadsDir);
            var ext = Path.GetExtension(model.ProfileImage.FileName);
            var fileName = $"{userId}{ext}";
            var filePath = Path.Combine(uploadsDir, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await model.ProfileImage.CopyToAsync(stream);
            imagePath = $"/images/profiles/{fileName}";
        }

        var result = await _updateHandler.HandleAsync(
            new UpdateUserProfileCommand(userId, model.FirstName, model.LastName, model.Email, model.PhoneNumber, imagePath));

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Error!);
            return View("Index", model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, model.Email),
            new(ClaimTypes.GivenName, model.FirstName),
            new(ClaimTypes.Surname, model.LastName),
        };
        if (model.PhoneNumber is not null)
            claims.Add(new Claim("PhoneNumber", model.PhoneNumber));
        if (imagePath is not null)
            claims.Add(new Claim("ProfileImagePath", imagePath));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        TempData["SuccessMessage"] = "Profile updated successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _deleteHandler.HandleAsync(new DeleteUserCommand(userId));
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
