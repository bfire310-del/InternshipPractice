using InternshipPractice.Api.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyByLikeWord;

public record GetVacancyByLikeWordQuery(string Word):IRequest<Result<List<VacancySearchResponse>>>;
