using InternshipPractice.Application.Interfaces.Services;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.ApproveApplication;

public class ApproveApplicationCommandHandler(IApplicationService applicationService) : IRequestHandler<ApproveApplicationCommand, Result>
{
    public async Task<Result> Handle(ApproveApplicationCommand request, CancellationToken cancellationToken)
    {
        var result = await applicationService.ApproveApplicationWithContract(request.UserId, request.ApplicationId);
        return result;
    }
}
