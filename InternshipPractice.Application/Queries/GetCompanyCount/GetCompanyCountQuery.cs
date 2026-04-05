using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompanyCount;

public record GetCompanyCountQuery():IRequest<Result<int>>;
