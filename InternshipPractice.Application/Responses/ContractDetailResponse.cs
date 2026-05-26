namespace InternshipPractice.Application.Responses;

public class ContractDetailResponse
{
    public Guid ContractId { get; init; }
    public string? ContractNumber { get; set; }
    public string? JobTitle { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public string? Student { get; init; }
    public string? Company { get; init; }
    public string? University { get; init; }
    public bool IsStudentSigned { get; init; }
    public bool IsEmployerSigned { get; init; }
    public bool IsUniversitySigned { get; init; }
    public string? ContractContent { get; init; }
}