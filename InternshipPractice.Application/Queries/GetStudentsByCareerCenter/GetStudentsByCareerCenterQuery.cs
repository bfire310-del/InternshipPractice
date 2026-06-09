using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetStudentsByCareerCenter;

public record GetStudentsByCareerCenterQuery(Guid userId):IRequest<Result<List<StudentResponse>>>;
