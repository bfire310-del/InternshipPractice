using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface ICompanyCategoryRepository
{
    Task<Result<List<NameDto>>> GetCompanyCategoryNameDtoList(string lang);
}
