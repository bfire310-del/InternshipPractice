using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompanyCategorNameDtoList;

public class GetCompanyCategorNameDtoListQueryHandler(ICompanyCategoryRepository companyCategoryRepository) : IRequestHandler<GetCompanyCategorNameDtoListQuery, Result<List<NameDto>>>
{
    public async Task<Result<List<NameDto>>> Handle(GetCompanyCategorNameDtoListQuery request, CancellationToken cancellationToken)
    {
        var result = await companyCategoryRepository.GetCompanyCategoryNameDtoList(request.Lang);
        return result;
    }
}
