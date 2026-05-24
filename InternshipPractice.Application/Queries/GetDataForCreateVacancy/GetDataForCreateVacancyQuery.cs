using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDataForCreateVacancy;

public record GetDataForCreateVacancyQuery():IRequest<Result<GetDataForCreateVacancyResponse>>;
