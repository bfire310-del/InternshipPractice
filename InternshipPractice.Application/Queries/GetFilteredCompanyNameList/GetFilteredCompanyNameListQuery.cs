using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetFilteredCompanyNameList;

public record GetFilteredCompanyNameListQuery(
    string? Query,
    Guid? RegionId,
    Guid? CategoryId,
    string Lang,
    int Page,
    int PageSize,
    Guid UserId
) : IRequest<Result<PagedResult<CompanySearchResponse>>>;