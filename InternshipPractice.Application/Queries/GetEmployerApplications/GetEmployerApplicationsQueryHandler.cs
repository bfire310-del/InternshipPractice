using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetEmployerApplications;

public class GetEmployerApplicationsQueryHandler(IEmployerApplicationRepository employerApplicationRepository)
    : IRequestHandler<GetEmployerApplicationsQuery, Result<PagedResult<EmployerApplicationResponse>>>
{
    public async Task<Result<PagedResult<EmployerApplicationResponse>>> Handle(
        GetEmployerApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        return await employerApplicationRepository.GetEmployerApplications(
            request.UserId,
            request.StatusCode,
            request.Lang,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}