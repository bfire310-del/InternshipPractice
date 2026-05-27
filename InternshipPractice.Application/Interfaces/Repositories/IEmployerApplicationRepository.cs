using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IEmployerApplicationRepository
{
    Task<Result<PagedResult<EmployerApplicationResponse>>> GetEmployerApplications(
        Guid userId,
        string? statusCode,
        string lang,
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}