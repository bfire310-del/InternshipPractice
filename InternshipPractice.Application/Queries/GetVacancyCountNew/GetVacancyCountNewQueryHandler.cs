using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCountNew;

public class GetVacancyCountNewQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetVacancyCountNewQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetVacancyCountNewQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetVacancyCountNew();
        return result;
    }
}
