using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDiaryForCareer;

public class GetDiaryForCareerQueryhandler(IStudentRepository studentRepository) : IRequestHandler<GetDiaryForCareerQuery, Result<List<CareerStudentApplicationResponse>>>
{
    public async Task<Result<List<CareerStudentApplicationResponse>>> Handle(GetDiaryForCareerQuery request, CancellationToken cancellationToken)
    {
        var result = await studentRepository.GetStudentApplicationsByCareerUserId(request.UserId);

        return result;
    }
}
