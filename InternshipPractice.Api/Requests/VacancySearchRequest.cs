namespace InternshipPractice.Api.Requests;

public class VacancySearchRequest
{
    public string? Query { get; init; }
    public Guid? RegionId { get; init; }
    public Guid? PaymentTypeId { get; init; } 
    public string? DurationCode { get; init; } 
    public Guid? CategoryId { get; init; }
    public string Lang { get; init; } = "ru";
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 5;   
}