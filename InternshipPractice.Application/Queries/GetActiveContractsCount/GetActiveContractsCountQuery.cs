using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetActiveContractsCount;

public record GetActiveContractsCountQuery(Guid UserId):IRequest<Result<int>>;
