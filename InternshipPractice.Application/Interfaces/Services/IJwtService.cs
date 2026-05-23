using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Services;

public interface IJwtService
{
    Task<Result<string>> GenerateToken(int userId, string roleCode);
    Task<Result<(int, string)>> ValidateToken(string token);
}
