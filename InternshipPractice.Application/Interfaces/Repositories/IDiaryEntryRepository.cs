using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Requests;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IDiaryEntryRepository
{
    Task<Result<List<DiaryEntryResponse>>> GetDiaryEntries(Guid userId);
    Task<Result> CreateOrUpdateDiaryEntry(
        Guid userId,
        CreateDiaryEntryRequest request);
}
