using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetRegionNameDtoList;

public class GetRegionNameDtoListQueryHandler(IRegionRepository regionRepository) : IRequestHandler<GetRegionNameDtoListQuery, Result<List<NameDto>>>
{
    public async Task<Result<List<NameDto>>> Handle(GetRegionNameDtoListQuery request, CancellationToken cancellationToken)
    {
        var result = await regionRepository.GetRegionNameDtoList(request.Lang);
        return result;
    }
}
