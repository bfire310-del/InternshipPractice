using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Services;

public interface IContractService
{
    Task<Result<SignContractResponse>> SignContract(
        Guid userId,
        Guid contractId,
        string signature,
        string lang,
        CancellationToken cancellationToken);
}