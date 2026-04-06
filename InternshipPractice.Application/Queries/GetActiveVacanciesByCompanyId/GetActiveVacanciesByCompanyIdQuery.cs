using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetActiveVacanciesByCompanyId;

public record GetActiveVacanciesByCompanyIdQuery(Guid CompanyId):IRequest<Result<int>>;
