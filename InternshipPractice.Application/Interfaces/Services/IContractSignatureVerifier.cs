using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Services;

public interface IContractSignatureVerifier
{
    Task<Result> VerifyAsync(
        string dataToSign,
        string signature,
        CancellationToken cancellationToken);
}