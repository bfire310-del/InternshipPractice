using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IEmployerRepository
{
    Task<Result<int>> GetEmployerCount();
}
