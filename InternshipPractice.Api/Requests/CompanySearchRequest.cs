namespace InternshipPractice.Api.Requests;

public class CompanySearchRequest
{
    public string? Query { get; init; }
    public Guid? RegionId { get; init; }
    public Guid? CategoryId { get; init; }
    public string Lang { get; init; } = "ru";
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 5;   
}