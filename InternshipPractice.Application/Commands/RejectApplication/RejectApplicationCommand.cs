using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.RejectApplication;

public record RejectApplicationCommand(
    Guid UserId,
    Guid ApplicationId
    ):IRequest<Result>;
