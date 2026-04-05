using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<Result<List<string?>>> GetCompanyNameList(string lang);
    Task<Result<int>> GetCompanyCount();
}
