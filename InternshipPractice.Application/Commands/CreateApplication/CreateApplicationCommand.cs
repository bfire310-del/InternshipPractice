using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.CreateApplication;

public record CreateApplicationCommand(
    Guid UserId,
    Guid VacancyId
    ):IRequest<Result>;
