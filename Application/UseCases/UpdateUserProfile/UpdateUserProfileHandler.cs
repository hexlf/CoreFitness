using Domain.Abstractions.Repositories;

namespace Application.UseCases.UpdateUserProfile;

public class UpdateUserProfileHandler
{
    private readonly IUserRepository _users;

    public UpdateUserProfileHandler(IUserRepository users) => _users = users;

    public async Task<UpdateUserProfileResult> HandleAsync(UpdateUserProfileCommand command, CancellationToken ct = default)
    {
        var user = await _users.GetByIdAsync(command.UserId, ct);
        if (user is null)
            return new UpdateUserProfileResult(false, "User not found.");

        var emailOwner = await _users.GetByEmailAsync(command.Email, ct);
        if (emailOwner is not null && emailOwner.Id != command.UserId)
            return new UpdateUserProfileResult(false, "This email address is already in use.");

        user.UpdateProfile(command.FirstName, command.LastName, command.Email, command.PhoneNumber, command.ProfileImagePath);
        await _users.SaveChangesAsync(ct);

        return new UpdateUserProfileResult(true);
    }
}