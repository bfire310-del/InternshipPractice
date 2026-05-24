using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractsCount;

public class GetContractsCountQueryHandler(IRegionRepository regionRepository) : IRequestHandler<GetContractsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetContractsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await regionRepository.GetRegionCount();
        return result;
    }
}
