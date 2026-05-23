using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetApplicationsByStatus;

public class GetApplicationsByStatusQueryHandler(IApplicationRepository applicationRepository) : IRequestHandler<GetApplicationsByStatusQuery, Result<List<ApplicationListResponse>>>
{
    public async Task<Result<List<ApplicationListResponse>>> Handle(GetApplicationsByStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await applicationRepository.GetApplicationsByStatus(request.UserId, request.StatusCode, request.Lang);
        return result;
    }
}
