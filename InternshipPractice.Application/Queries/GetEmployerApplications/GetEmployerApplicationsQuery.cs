using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetEmployerApplications;

public record GetEmployerApplicationsQuery(
    Guid UserId,
    string? StatusCode,
    string Lang,
    int Page,
    int PageSize) : IRequest<Result<PagedResult<EmployerApplicationResponse>>>;