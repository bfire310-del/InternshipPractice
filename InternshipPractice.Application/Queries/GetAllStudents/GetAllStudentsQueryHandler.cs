using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetAllStudents;

public class GetAllStudentsQueryHandler(IStudentRepository studentRepository) : IRequestHandler<GetAllStudentsQuery, Result<List<StudentResponse>>>
{
    public async Task<Result<List<StudentResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var result = await studentRepository.GetAll();
        return result;
    }
}
