namespace InternshipPractice.Application.Responses;

public class ContractResponse
{
    public Guid ContractId { get; init; }
    public string? Status { get; init; }
    public string? JobTitle { get; init; }
    public string? Student { get; init; }
    public string? CompanyName { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}