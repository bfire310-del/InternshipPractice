using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IRegionRepository
{
    Task<Result<int>> GetRegionCount();
}
