using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacanciesByEmployerId;

public class GetVacanciesByEmployerIdQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetVacanciesByEmployerIdQuery, Result<List<VacancySearchResponse>>>
{
    public async Task<Result<List<VacancySearchResponse>>> Handle(GetVacanciesByEmployerIdQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetVacanciesByEmployerId(request.EmployerId);

        return result;
    }
}
