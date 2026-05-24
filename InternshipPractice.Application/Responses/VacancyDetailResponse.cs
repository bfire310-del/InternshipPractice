namespace InternshipPractice.Application.Responses;

public class VacancyDetailResponse
{
    public Guid VacancyId { get; init; }
    public string JobTitle { get; init; } = "";
    public string? Status { get; init; }
    public string? CompanyName { get; init; }
    public string? CategoryName { get; init; }
    public string? WorkFormatName { get; init; }
    public string? ShortDescription { get; init; }
    public string? FullDescription { get; init; }
    public string? Requirements { get; init; }
    public string? RegionName { get; init; }
    public string? Duration { get; set; }
    public string? PaymentType { get; init; }
    public string? TypeOfEmployment { get; init; }
    public DateTime? CreatedAt { get; init; }
    public List<string> Skills { get; init; } = [];
    public bool HasApplied { get; init; }
}