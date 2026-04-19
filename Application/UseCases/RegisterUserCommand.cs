namespace Application.UseCases.RegisterUser;

public record RegisterUserCommand(string Email, string Password, string FirstName, string LastName);

public record RegisterUserResult(bool Success, string? Error = null, Guid? UserId = null);