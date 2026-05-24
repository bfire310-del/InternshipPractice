using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IVacancyCategoryRepository
{
    Task<Result<List<NameDto>>> GetVacancyCategoryNameDtoList(string lang);
    Task<Result<List<VacancyCategoryDto>>> GetAll();
}
