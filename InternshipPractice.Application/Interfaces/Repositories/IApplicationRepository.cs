
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IApplicationRepository
{
    Task<Result> CreateApplication(Guid userId, Guid vacancyId);
    Task<Result<PagedResult<ApplicationListResponse>>> GetApplicationsByStatus(Guid userId, string? statusCode, string lang, int page, int pageSize);
    Task<Result> WithdrawApplication(Guid userId, Guid applicationId);
    Task<Result> ApproveApplicationWithoutSave(Guid userId, Guid applicationId);
    Task<Result> RejectApplication(Guid userId, Guid applicationId);
}
