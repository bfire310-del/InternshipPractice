using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDiaryDetails;

public record GetDiaryDetailsQuery(Guid UserId, Guid StudentId):IRequest<Result<CareerDiaryDetailsResponse>>;
