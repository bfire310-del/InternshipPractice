using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.CreateApplication;

public class CreateApplicationCommandHandler(IApplicationRepository applicationRepository) : IRequestHandler<CreateApplicationCommand, Result>
{
    public async Task<Result> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var result = await applicationRepository.CreateApplication(request.UserId, request.VacancyId);
        return result;
    }
}
