using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.RejectApplication;

public class RejectApplicationCommandHandler(IApplicationRepository applicationRepository) : IRequestHandler<RejectApplicationCommand, Result>
{
    public async Task<Result> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
    {
        var result = await applicationRepository.RejectApplication(request.UserId, request.ApplicationId);
        return result;
    }
}
