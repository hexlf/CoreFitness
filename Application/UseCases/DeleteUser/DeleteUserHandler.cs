using Domain.Abstractions.Repositories;

namespace Application.UseCases.DeleteUser;

public class DeleteUserHandler
{
    private readonly IUserRepository _users;

    public DeleteUserHandler(IUserRepository users) => _users = users;

    public async Task<DeleteUserResult> HandleAsync(DeleteUserCommand command, CancellationToken ct = default)
    {
        var user = await _users.GetByIdAsync(command.UserId, ct);
        if (user is null)
            return new DeleteUserResult(false, "User not found.");

        await _users.DeleteAsync(user, ct);
        await _users.SaveChangesAsync(ct);

        return new DeleteUserResult(true);
    }
}