using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractsCount;

public record GetContractsCountQuery(Guid UserId):IRequest<Result<int>>;
