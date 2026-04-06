using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetActiveVacanciesByCompanyId;

public class GetActiveVacanciesByCompanyIdQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetActiveVacanciesByCompanyIdQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetActiveVacanciesByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetActiveVacanciesCountByCompanyId(request.CompanyId);

        return result;
    }
}
