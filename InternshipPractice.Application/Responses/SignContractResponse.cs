namespace InternshipPractice.Application.Responses;

public class SignContractResponse
{
    public Guid ContractId { get; init; }
    public string StatusCode { get; init; } = null!;
    public string Message { get; init; } = null!;
}