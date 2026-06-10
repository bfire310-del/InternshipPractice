using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDiaryDetails;

public class GetDiaryDetailsQueryHandler(IDiaryEntryRepository diaryEntryRepository) : IRequestHandler<GetDiaryDetailsQuery, Result<CareerDiaryDetailsResponse>>
{
    public async Task<Result<CareerDiaryDetailsResponse>> Handle(GetDiaryDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await diaryEntryRepository.GetDiaryDetails(request.UserId, request.StudentId);

        return result;
    }
}
