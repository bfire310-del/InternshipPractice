namespace InternshipPractice.Application.Responses;

public class ContractSignDataResponse
{
    public Guid ContractId { get; init; }
    public string ContractNumber { get; init; } = null!;
    public string DataToSign { get; init; } = null!;
}