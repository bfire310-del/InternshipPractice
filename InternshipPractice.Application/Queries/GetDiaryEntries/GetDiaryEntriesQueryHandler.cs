using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDiaryEntries;

public class GetDiaryEntriesQueryHandler(IDiaryEntryRepository diaryEntryRepository) : IRequestHandler<GetDiaryEntriesQuery, Result<List<DiaryEntryResponse>>>
{
    public async Task<Result<List<DiaryEntryResponse>>> Handle(GetDiaryEntriesQuery request, CancellationToken cancellationToken)
    {
        var result = await diaryEntryRepository.GetDiaryEntries(request.UserId);
        return result;
    }
}
