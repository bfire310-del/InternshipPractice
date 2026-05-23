using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IVacancyCategoryRepository
{
    Task<Result<List<NameDto>>> GetVacancyCategoryNameDtoList(string lang);
}
