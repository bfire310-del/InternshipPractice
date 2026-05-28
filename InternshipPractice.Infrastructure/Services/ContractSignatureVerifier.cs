using System.Security.Cryptography.Pkcs;
using System.Text;
using InternshipPractice.Application.Interfaces.Services;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Infrastructure.Services;

public class ContractSignatureVerifier : IContractSignatureVerifier
{
    public Task<Result> VerifyAsync(
        string dataToSign,
        string signature,
        CancellationToken cancellationToken)
    {
        try
        {
            var cleanSignature = NormalizeCmsSignature(signature);

            var cmsBytes = Convert.FromBase64String(cleanSignature);
            var contentBytes = Encoding.UTF8.GetBytes(dataToSign);

            var signedCms = new SignedCms(
                new ContentInfo(contentBytes),
                detached: true);

            signedCms.Decode(cmsBytes);

            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            return Task.FromResult(
                Result.Failure(
                    new Error(
                        Domain.Common.Error.BadRequest,
                        $"Подпись недействительна: {ex.Message}")));
        }
    }

    private static string NormalizeCmsSignature(string signature)
    {
        return signature
            .Replace("-----BEGIN CMS-----", "")
            .Replace("-----END CMS-----", "")
            .Replace("\r", "")
            .Replace("\n", "")
            .Replace(" ", "")
            .Trim();
    }
}