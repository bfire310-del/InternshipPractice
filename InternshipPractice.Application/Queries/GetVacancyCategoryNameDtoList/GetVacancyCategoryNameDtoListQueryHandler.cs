using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCategoryNameDtoList;

public class GetVacancyCategoryNameDtoListQueryHandler(IVacancyCategoryRepository vacancyCategoryRepository) : IRequestHandler<GetVacancyCategoryNameDtoListQuery, Result<List<NameDto>>>
{
    public async Task<Result<List<NameDto>>> Handle(GetVacancyCategoryNameDtoListQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyCategoryRepository.GetVacancyCategoryNameDtoList(request.Lang);
        return result;
    }
}
