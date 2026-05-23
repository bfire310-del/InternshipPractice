using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.WithdrawApplication;

public record WithdrawApplicationCommand(
    Guid UserId,
    Guid ApplicationId
    ):IRequest<Result>;
