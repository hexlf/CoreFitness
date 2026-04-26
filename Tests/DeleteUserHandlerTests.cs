using Application.UseCases.DeleteUser;
using Domain.Abstractions.Repositories;
using Domain.Aggregates;
using Moq;

namespace Tests;

public class DeleteUserHandlerTests
{
    private readonly Mock<IUserRepository> _repoMock = new();

    private DeleteUserHandler CreateSut() => new(_repoMock.Object);

    private static User CreateTestUser() =>
        User.Create("test@test.com", "hash", "Test", "User");

    [Fact]
    public async Task HandleAsync_UserNotFound_ReturnsFailure()
    {
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync((User?)null);

        var result = await CreateSut().HandleAsync(new DeleteUserCommand(Guid.NewGuid()));

        Assert.False(result.Success);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task HandleAsync_UserExists_CallsDeleteAndSave()
    {
        var user = CreateTestUser();
        _repoMock.Setup(r => r.GetByIdAsync(user.Id, default)).ReturnsAsync(user);
        _repoMock.Setup(r => r.DeleteAsync(user, default)).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync(default)).Returns(Task.CompletedTask);

        var result = await CreateSut().HandleAsync(new DeleteUserCommand(user.Id));

        Assert.True(result.Success);
        _repoMock.Verify(r => r.DeleteAsync(user, default), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(default), Times.Once);
    }
}