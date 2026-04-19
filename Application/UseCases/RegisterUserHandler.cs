using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Aggregates;

namespace Application.UseCases.RegisterUser;

public class RegisterUserHandler
{
    private readonly IUserRepository _users;
    private readonly IPasswordService _passwords;

    public RegisterUserHandler(IUserRepository users, IPasswordService passwords)
    {
        _users = users;
        _passwords = passwords;
    }

    public async Task<RegisterUserResult> HandleAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        if (await _users.ExistsByEmailAsync(command.Email, ct))
            return new RegisterUserResult(false, "An account with this email already exists.");

        var hash = _passwords.Hash(command.Password);
        var user = User.Create(command.Email, hash, command.FirstName, command.LastName);

        await _users.AddAsync(user, ct);
        await _users.SaveChangesAsync(ct);

        return new RegisterUserResult(true, UserId: user.Id);
    }
}