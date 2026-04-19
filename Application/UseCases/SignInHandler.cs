using Application.Services;
using Domain.Abstractions.Repositories;

namespace Application.UseCases.SignIn;

public class SignInHandler
{
    private readonly IUserRepository _users;
    private readonly IPasswordService _passwords;

    public SignInHandler(IUserRepository users, IPasswordService passwords)
    {
        _users = users;
        _passwords = passwords;
    }

    public async Task<SignInResult> HandleAsync(SignInCommand command, CancellationToken ct = default)
    {
        var user = await _users.GetByEmailAsync(command.Email, ct);
        if (user is null || !_passwords.Verify(command.Password, user.PasswordHash))
            return new SignInResult(false, "Invalid email or password.");

        return new SignInResult(true, Email: user.Email, FirstName: user.FirstName, LastName: user.LastName, UserId: user.Id);
    }
}