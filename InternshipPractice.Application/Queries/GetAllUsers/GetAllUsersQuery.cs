using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetAllUsers;

public record GetAllUsersQuery():IRequest<Result<List<UserResponse>>>;
