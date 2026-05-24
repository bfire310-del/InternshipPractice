using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.DeleteVacancy;

public class DeleteVacancyQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<DeleteVacancyQuery, Result>
{
    public async Task<Result> Handle(DeleteVacancyQuery request, CancellationToken cancellationToken)
    {
        var delete = await vacancyRepository.Delete(request.VacancyId);
        return delete;
    }
}
