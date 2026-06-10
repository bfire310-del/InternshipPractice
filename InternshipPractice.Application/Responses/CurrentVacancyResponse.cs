namespace InternshipPractice.Application.Responses;

public class CurrentVacancyResponse
{
    public Guid? VacancyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string SupervisorFullName { get; set; } = null!;
}