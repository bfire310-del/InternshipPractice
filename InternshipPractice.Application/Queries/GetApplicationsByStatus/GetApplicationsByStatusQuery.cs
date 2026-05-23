using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetApplicationsByStatus;

public record GetApplicationsByStatusQuery(Guid UserId, string? StatusCode, string Lang):IRequest<Result<List<ApplicationListResponse>>>;
