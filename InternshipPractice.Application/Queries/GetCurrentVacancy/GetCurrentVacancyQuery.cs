using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCurrentVacancy;

public record GetCurrentVacancyQuery(
    Guid UserId
) : IRequest<Result<CurrentVacancyResponse>>;