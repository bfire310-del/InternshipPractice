using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.CreateApplication;

public record CreateApplicationCommand(
    Guid UserId,
    Guid VacancyId
    ):IRequest<Result>;
