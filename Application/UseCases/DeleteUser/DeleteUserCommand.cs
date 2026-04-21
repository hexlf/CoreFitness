
namespace Application.UseCases.DeleteUser;

public record DeleteUserCommand(Guid UserId);

public record DeleteUserResult(bool Success, string? Error = null);