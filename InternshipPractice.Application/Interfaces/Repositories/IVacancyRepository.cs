using InternshipPractice.Api.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IVacancyRepository
{
    Task<Result<int>> GetVacancyCount();
    Task<Result<int>> GetVacancyCountNew();
    Task<Result<PagedResult<VacancySearchResponse>>> GetFilteredVacancyNamesAsync(
        string? query,
        Guid? regionId,
        Guid? categoryId,
        Guid? workFormatId,
        Guid? practiceFormId,
        Guid? typeOfEmploymentId,
        int? course,
        bool onlyPublished,
        bool? onlyPaid,
        int? durationMonthsMin,
        int? durationMonthsMax,
        string lang,
        int page,
        int pageSize,
        CancellationToken ct);
}
