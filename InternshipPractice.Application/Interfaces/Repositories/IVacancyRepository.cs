using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IVacancyRepository
{
    Task<Result<int>> GetVacancyCount();
    Task<Result<int>> GetVacancyCountNew();
    Task<Result<PagedResult<VacancySearchResponse>>> GetFilteredVacancyNamesAsync(
        string? query,
        Guid? regionId,
        Guid? paymentTypeId,
        string? durationCode,
        Guid? categoryId,
        string lang,
        int page,
        int pageSize,
        Guid userId,
        CancellationToken ct);
    Task<Result<List<VacancySearchResponse>>> GetVacancyByLikeWord(string word);
    Task<Result<int>> GetActiveVacanciesCountByCompanyId(Guid companyId);
    Task<Result<List<VacancySearchResponse>>> GetVacanciesByEmployerId(Guid employerId);
    Task<Result<VacancyDetailResponse>> GetVacancyDetailsById(Guid id, Guid userId);
}
