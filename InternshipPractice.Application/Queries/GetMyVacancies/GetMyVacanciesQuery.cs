using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetMyVacancies;

public record GetMyVacanciesQuery(Guid UserId):IRequest<Result<List<GetMyVacanciesResponse>>>;
