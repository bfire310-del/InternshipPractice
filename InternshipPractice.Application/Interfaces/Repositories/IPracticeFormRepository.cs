using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IPracticeFormRepository
{
    Task<Result<List<PracticeFormDto>>> GetAll();
}
