using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCountNew;

public record GetVacancyCountNewQuery():IRequest<Result<int>>;
