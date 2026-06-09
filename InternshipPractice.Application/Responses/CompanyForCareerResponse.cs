namespace InternshipPractice.Application.Responses;

public class CompanyForCareerResponse
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;
    public string? Description { get; set; }

    public Guid? RegionId { get; set; }
    public string? RegionName { get; set; }

    public Guid? CompanyCategoryId { get; set; }
    public string? CompanyCategoryName { get; set; }

    public int VacanciesCount { get; set; }

    public string? ContactFirstName { get; set; }
    public string? ContactLastName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
}
