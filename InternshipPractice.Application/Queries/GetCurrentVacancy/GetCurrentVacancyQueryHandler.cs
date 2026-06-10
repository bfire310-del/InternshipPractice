using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCurrentVacancy;

public class GetCurrentVacancyQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetCurrentVacancyQuery, Result<CurrentVacancyResponse>>
{
    public async Task<Result<CurrentVacancyResponse>> Handle(GetCurrentVacancyQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetCurrentVacancy(request.UserId);
        return result;
    }
}
