using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<Result<List<string?>>> GetCompanyNameList(string lang);
    Task<Result<int>> GetCompanyCount();
    Task<Result<List<CompanyResponse>>> GetAll();
}
