using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IContractRepository
{
    Task<Result> GenerateContractWithoutSave(Guid userId, Guid applicationId);
}
