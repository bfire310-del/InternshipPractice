using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetWaitingForSignContractsCount;

public class GetWaitingForSignContractsCountQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetWaitingForSignContractsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetWaitingForSignContractsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetWaitingForSignContractsCount(request.UserId);
        return result;
    }
}
