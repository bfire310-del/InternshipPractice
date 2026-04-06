using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetAllStudents;

public record GetAllStudentsQuery() : IRequest<Result<List<StudentResponse>>>;
