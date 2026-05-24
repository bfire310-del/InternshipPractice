using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetFilteredVacancyNameList;

public class GetFilteredVacancyNameListQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetFilteredVacancyNameListQuery, Result<PagedResult<VacancySearchResponse>>>
{
    public async Task<Result<PagedResult<VacancySearchResponse>>> Handle(GetFilteredVacancyNameListQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetFilteredVacancyNamesAsync(request.Query,
            request.RegionId,
            request.PaymentTypeId,
            request.DurationCode,
            request.CategoryId,
            request.Lang,
            request.Page,
            request.PageSize,
            request.UserId,
            cancellationToken);
        return result;
    }
}
