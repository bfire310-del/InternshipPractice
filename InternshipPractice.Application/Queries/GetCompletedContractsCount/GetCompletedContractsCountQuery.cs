using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompletedContractsCount;

public record GetCompletedContractsCountQuery(Guid UserId):IRequest<Result<int>>;
