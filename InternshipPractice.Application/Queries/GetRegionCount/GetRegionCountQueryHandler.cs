using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetRegionCount;

public class GetRegionCountQueryHandler(IRegionRepository regionRepository) : IRequestHandler<GetRegionCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetRegionCountQuery request, CancellationToken cancellationToken)
    {
        var result = await regionRepository.GetRegionCount();
        return result;
    }
}
