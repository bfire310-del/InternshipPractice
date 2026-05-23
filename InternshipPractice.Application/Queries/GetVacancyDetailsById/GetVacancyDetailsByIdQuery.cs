using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyDetailsById;

public record GetVacancyDetailsByIdQuery(Guid Id, Guid UserId):IRequest<Result<VacancyDetailResponse>>;
