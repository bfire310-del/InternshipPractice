using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDiaryEntries;

public record GetDiaryEntriesQuery(
    Guid UserId
) : IRequest<Result<List<DiaryEntryResponse>>>;