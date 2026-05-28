namespace InternshipPractice.Application.Responses;

public class CompanySearchResponse
{
    public Guid CompanyId { get; init; }
    public string? Name { get; init; }
    public string? Website { get; init; }
    public string? CategoryName { get; init; }
    public string? Description { get; init; }
    public string? RegionName { get; init; }
    public int? VacancyCount { get; set; }
    public DateTime? CreatedAt { get; init; } 
}