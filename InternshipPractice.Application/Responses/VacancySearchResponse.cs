namespace InternshipPractice.Api.Responses;

public class VacancySearchResponse
{
    public Guid VacancyId { get; init; }
    public string Title { get; init; } = "";
    public string? CompanyName { get; init; }
    public string? ShortDescription { get; init; }

    public Guid? RegionId { get; init; }
    public string? RegionName { get; init; }

    public Guid? CategoryId { get; init; }
    public string? CategoryName { get; init; }

    public Guid? WorkFormatId { get; init; }
    public string? Payment { get; init; }

    public DateTime? CreatedAt { get; init; } 
}