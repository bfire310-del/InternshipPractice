using KDS.Primitives.FluentResult;

namespace InternshipPractice.Domain.Interfaces.Repositories;

public interface IVacancyCategoryRepository
{
    Task<Result<List<string?>>> GetVacancyCategoryNameList(string lang);
}
