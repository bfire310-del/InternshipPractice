using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.Login;

public record LoginQuery(string Email, string Password):IRequest<Result<string>>;
