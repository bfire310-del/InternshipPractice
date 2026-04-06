using InternshipPractice.Api.Responses;
using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyByLikeWord;

public class GetVacancyByLikeWordQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetVacancyByLikeWordQuery, Result<List<VacancySearchResponse>>>
{
    public async Task<Result<List<VacancySearchResponse>>> Handle(GetVacancyByLikeWordQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetVacancyByLikeWord(request.Word);
        return result;
    }
}
