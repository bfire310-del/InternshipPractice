using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCount;

public record GetVacancyCountQuery():IRequest<Result<int>>;
