using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCount;

public class GetVacancyCountQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetVacancyCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetVacancyCountQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetVacancyCount();
        return result;
    }
}
