using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Services;

public interface IApplicationService
{
    Task<Result> ApproveApplicationWithContract(Guid userId, Guid applicationId);
}
