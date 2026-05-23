
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IApplicationRepository
{
    Task<Result> CreateApplication(Guid userId, Guid vacancyId);
}
