using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompletedContractsCount;

public class GetCompletedContractsCountQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetCompletedContractsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetCompletedContractsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetCompletedContractsCount(request.UserId);
        return result;
    }
}
