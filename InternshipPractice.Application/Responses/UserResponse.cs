namespace InternshipPractice.Application.Responses;

public class UserResponse
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}
