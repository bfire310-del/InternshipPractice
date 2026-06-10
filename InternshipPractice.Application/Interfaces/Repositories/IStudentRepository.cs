using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Result<List<StudentResponse>>> GetAll();
    Task<Result<List<StudentResponse>>> GetStudentsByCareerUserId(Guid userId);
    Task<Result<List<CareerStudentApplicationResponse>>> GetStudentApplicationsByCareerUserId(Guid userId);
}
