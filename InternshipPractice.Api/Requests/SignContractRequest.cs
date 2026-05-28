namespace InternshipPractice.Api.Requests;

public class SignContractRequest
{
    public Guid ContractId { get; init; }
    public string Signature { get; init; } = null!;
    public string Lang { get; init; } = "ru";
}