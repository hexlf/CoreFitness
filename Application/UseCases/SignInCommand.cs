namespace Application.UseCases.SignIn;

public record SignInCommand(string Email, string Password);
public record SignInResult(bool Success, string? Error = null, Guid? UserId = null, string? Email = null, string? FirstName = null, string? LastName = null);