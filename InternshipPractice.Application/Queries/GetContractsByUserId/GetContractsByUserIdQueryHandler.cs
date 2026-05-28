using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractsByUserId;

public class GetContractsByUserIdQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetContractsByUserIdQuery, Result<PagedResult<ContractResponse>>>
{
    public async Task<Result<PagedResult<ContractResponse>>> Handle(GetContractsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetContractsByUserId(request.UserId, request.Lang, request.Page, request.PageSize);
        return result;
    }
}
