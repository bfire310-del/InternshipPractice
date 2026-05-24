using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface ITypeOfEmploymentRepository
{
    Task<Result<List<TypeOfEmploymentDto>>> GetAll();
}
