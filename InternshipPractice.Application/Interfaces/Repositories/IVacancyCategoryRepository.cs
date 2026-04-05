using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IVacancyCategoryRepository
{
    Task<Result<List<string?>>> GetVacancyCategoryNameList(string lang);
}
