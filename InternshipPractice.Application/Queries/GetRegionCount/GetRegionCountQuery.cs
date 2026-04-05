using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetRegionCount;

public record GetRegionCountQuery():IRequest<Result<int>>;
