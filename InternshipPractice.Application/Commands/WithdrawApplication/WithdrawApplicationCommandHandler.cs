using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.WithdrawApplication;

public class WithdrawApplicationCommandHandler(IApplicationRepository applicationRepository) : IRequestHandler<WithdrawApplicationCommand, Result>
{
    public async Task<Result> Handle(WithdrawApplicationCommand request, CancellationToken cancellationToken)
    {
        var result = await applicationRepository.WithdrawApplication(request.UserId, request.ApplicationId);
        return result;
    }
}
