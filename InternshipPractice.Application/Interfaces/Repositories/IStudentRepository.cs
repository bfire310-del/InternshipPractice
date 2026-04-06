using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Result<List<StudentResponse>>> GetAll();
}
