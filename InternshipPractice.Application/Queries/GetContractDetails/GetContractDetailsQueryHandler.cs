using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractDetails;

public class GetContractDetailsQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetContractDetailsQuery, Result<ContractDetailResponse>>
{
    public async Task<Result<ContractDetailResponse>> Handle(GetContractDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetContractDetails(request.UserId, request.Lang, request.ContractId);
        return result;
    }
}
