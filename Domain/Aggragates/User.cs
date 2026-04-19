namespace Domain.Aggregates;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = "";
    public string PasswordHash { get; private set; } = "";
    public string FirstName { get; private set; } = "";
    public string LastName { get; private set; } = "";
    public DateTime CreatedAt { get; private set; }

    private User() { }

    public static User Create(string email, string passwordHash, string firstName, string lastName)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email.ToLowerInvariant(),
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            CreatedAt = DateTime.UtcNow
        };
    }
}