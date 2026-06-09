using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetStudentsByCareerCenter;

public class GetStudentsByCareerCenterQueryHandler(IStudentRepository studentRepository) : IRequestHandler<GetStudentsByCareerCenterQuery, Result<List<StudentResponse>>>
{
    public async Task<Result<List<StudentResponse>>> Handle(GetStudentsByCareerCenterQuery request, CancellationToken cancellationToken)
    {
        var result = await studentRepository.GetStudentsByCareerUserId(request.userId);

        return result;
    }
}
