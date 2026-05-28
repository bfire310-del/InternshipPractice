using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetWaitingForSignContractsCount;

public record GetWaitingForSignContractsCountQuery(Guid UserId):IRequest<Result<int>>;
