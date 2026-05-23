using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyDetailsById;

public record GetVacancyDetailsByIdQuery(Guid Id):IRequest<Result<VacancyDetailResponse>>;
