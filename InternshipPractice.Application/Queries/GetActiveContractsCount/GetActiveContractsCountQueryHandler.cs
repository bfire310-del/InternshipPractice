using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetActiveContractsCount;

public class GetActiveContractsCountQueryHandler(IRegionRepository regionRepository) : IRequestHandler<GetActiveContractsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetActiveContractsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await regionRepository.GetRegionCount();
        return result;
    }
}
