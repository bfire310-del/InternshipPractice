using InternshipPractice.Domain.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCategoryNameList;

public class GetVacancyCategoryNameListQueryHandler(IVacancyCategoryRepository vacancyCategoryRepository) : IRequestHandler<GetVacancyCategoryNameListQuery, Result<List<string>>>
{
    public async Task<Result<List<string>>> Handle(GetVacancyCategoryNameListQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyCategoryRepository.GetVacancyCategoryNameList(request.Lang);
        return result;
    }
}
