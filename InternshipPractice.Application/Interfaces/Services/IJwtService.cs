using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Services;

public interface IJwtService
{
    Task<Result<string>> GenerateToken(Guid userId, string roleCode);
    Task<Result<(Guid, string)>> ValidateToken(string token);
}
