using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractsByUserId;

public class GetContractsByUserIdQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetContractsByUserIdQuery, Result<PagedResult<VacancySearchResponse>>>
{
    public async Task<Result<PagedResult<VacancySearchResponse>>> Handle(GetContractsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return Result.Failure<PagedResult<VacancySearchResponse>>(new Error("", ""));
    }
}
