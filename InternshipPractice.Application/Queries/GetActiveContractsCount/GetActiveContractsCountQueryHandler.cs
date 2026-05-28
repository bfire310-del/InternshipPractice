using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetActiveContractsCount;

public class GetActiveContractsCountQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetActiveContractsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetActiveContractsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetActiveContractsCount(request.UserId);
        return result;
    }
}
