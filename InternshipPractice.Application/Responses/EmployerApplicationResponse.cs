namespace InternshipPractice.Application.Responses;

public class EmployerApplicationResponse
{
    public Guid ApplicationId { get; init; }
    public Guid? VacancyId { get; init; }
    public Guid? StudentId { get; init; }
    public Guid? StudentUserId { get; init; }
    public string StudentFullName { get; init; } = "";
    public string? StudentEmail { get; init; }
    public string? StudentPhone { get; init; }
    public int? Course { get; init; }
    public decimal? Gpa { get; init; }
    public string? FacultyName { get; init; }
    public string? StudentStatus { get; init; }
    public List<string> Skills { get; init; } = [];
    public string JobTitle { get; init; } = "";
    public string? VacancyName { get; init; }
    public string? Status { get; init; }
    public string? StatusCode { get; init; }
    public DateTime? CreatedAt { get; init; }
}