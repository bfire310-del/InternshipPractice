using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetEmployerCount;

public record GetEmployerCountQuery():IRequest<Result<int>>;
