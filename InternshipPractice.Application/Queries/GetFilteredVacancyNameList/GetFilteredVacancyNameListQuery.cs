using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetFilteredVacancyNameList;

public record GetFilteredVacancyNameListQuery(
    string? Query,
    Guid? RegionId,
    Guid? PaymentTypeId,
    string? DurationCode,
    Guid? CategoryId,
    string Lang,
    int Page,
    int PageSize
) : IRequest<Result<PagedResult<VacancySearchResponse>>>;