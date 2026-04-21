namespace Application.UseCases.UpdateUserProfile;

public record UpdateUserProfileCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? ProfileImagePath
);

public record UpdateUserProfileResult(bool Success, string? Error = null);