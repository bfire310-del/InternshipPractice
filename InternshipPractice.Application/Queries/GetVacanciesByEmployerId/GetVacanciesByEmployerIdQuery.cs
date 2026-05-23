using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacanciesByEmployerId;

public record GetVacanciesByEmployerIdQuery(Guid EmployerId):IRequest<Result<List<VacancySearchResponse>>>;
