using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDiaryForCareer;

public record GetDiaryForCareerQuery(Guid UserId):IRequest<Result<List<CareerStudentApplicationResponse>>>;
