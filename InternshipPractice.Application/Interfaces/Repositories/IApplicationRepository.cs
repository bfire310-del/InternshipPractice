
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IApplicationRepository
{
    Task<Result> CreateApplication(Guid userId, Guid vacancyId);
    Task<Result<List<ApplicationListResponse>>> GetApplicationsByStatus(Guid userId, string? statusCode, string lang);
    Task<Result> WithdrawApplication(Guid userId, Guid applicationId);
    Task<Result<int>> GetResponsesCountByEmployerId(Guid userId);
    Task<Result<int>> GetCongratsContracts(Guid userId);
}
