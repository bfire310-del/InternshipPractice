using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.CreateVacancy;

public class CreateVacancyCommandHandler(IVacancyRepository vacancyRepository) : IRequestHandler<CreateVacancyCommand, Result>
{
    public async Task<Result> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
    {
        var add = await vacancyRepository.AddAsync(request.Request);

        return add;
    }
}
