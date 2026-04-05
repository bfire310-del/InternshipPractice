namespace InternshipPractice.Api.Requests;

public class VacancySearchRequest
{
    public string? Query { get; init; }
    public Guid? RegionId { get; init; }
    public Guid? CategoryId { get; init; }
    public Guid? WorkFormatId { get; init; } 
    public Guid? PracticeFormId { get; init; }
    public Guid? TypeOfEmploymentId { get; init; }

    public int? Course { get; init; }
    public bool? OnlyPublished { get; init; } = true;

    public bool? OnlyPaid { get; init; } 
    
    public int? DurationMonthsMin { get; init; }
    public int? DurationMonthsMax { get; init; }

    public string Lang { get; init; } = "ru";
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 5;   
}