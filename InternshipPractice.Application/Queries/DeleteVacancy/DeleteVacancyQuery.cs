using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries;

public record DeleteVacancyQuery(Guid VacancyId):IRequest<Result>;
