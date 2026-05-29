using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractsByStuff;

public class GetContractsByStuffQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetContractsByStuffQuery, Result<PagedResult<ContractResponse>>>
{
    public async Task<Result<PagedResult<ContractResponse>>> Handle(GetContractsByStuffQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetContractsByStuff(request.UserId, request.Lang, request.Page, request.PageSize);
        return result;
    }
}
