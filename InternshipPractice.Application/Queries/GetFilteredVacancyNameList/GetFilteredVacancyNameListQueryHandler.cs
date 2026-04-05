using InternshipPractice.Api.Responses;
using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetFilteredVacancyNameList;

public class GetFilteredVacancyNameListQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetFilteredVacancyNameListQuery, Result<PagedResult<VacancySearchResponse>>>
{
    public async Task<Result<PagedResult<VacancySearchResponse>>> Handle(GetFilteredVacancyNameListQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetFilteredVacancyNamesAsync(request.Query,
            request.RegionId,
            request.CategoryId,
            request.WorkFormatId,
            request.PracticeFormId,
            request.TypeOfEmploymentId,
            request.Course,
            request.OnlyPublished,
            request.OnlyPaid,
            request.DurationMonthsMin,
            request.DurationMonthsMax,
            request.Lang,
            request.Page,
            request.PageSize,
            cancellationToken);
        return result;
    }
}
