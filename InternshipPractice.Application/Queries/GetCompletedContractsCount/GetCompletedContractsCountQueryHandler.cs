using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompletedContractsCount;

public class GetCompletedContractsCountQueryHandler(IRegionRepository regionRepository) : IRequestHandler<GetCompletedContractsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetCompletedContractsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await regionRepository.GetRegionCount();
        return result;
    }
}
