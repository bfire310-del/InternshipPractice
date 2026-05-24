using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.ApproveApplication;

public record ApproveApplicationCommand(
    Guid UserId,
    Guid ApplicationId
    ):IRequest<Result>;
