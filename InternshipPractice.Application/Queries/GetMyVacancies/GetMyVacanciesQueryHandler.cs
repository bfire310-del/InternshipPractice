using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetMyVacancies;

public class GetMyVacanciesQueryHandler(IVacancyRepository vacancyRepository
    ) : IRequestHandler<GetMyVacanciesQuery, Result<List<GetMyVacanciesResponse>>>
{
    public async Task<Result<List<GetMyVacanciesResponse>>> Handle(GetMyVacanciesQuery request, CancellationToken cancellationToken)
    {
        var vacancy = await vacancyRepository.GetMyVacancies(request.UserId);

        return vacancy;
    }
}
