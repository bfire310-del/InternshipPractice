using InternshipPractice.Api.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetFilteredVacancyNameList;

public record GetFilteredVacancyNameListQuery(
    string? Query,
    Guid? RegionId,
    Guid? CategoryId,
    Guid? WorkFormatId,
    Guid? PracticeFormId,
    Guid? TypeOfEmploymentId,
    int? Course,
    bool OnlyPublished,
    bool? OnlyPaid,
    int? DurationMonthsMin,
    int? DurationMonthsMax,
    string Lang,
    int Page,
    int PageSize
) : IRequest<Result<PagedResult<VacancySearchResponse>>>;