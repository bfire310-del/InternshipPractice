using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Interfaces.Services;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.Login;

public class LoginQueryHandler(IUsersRepository usersRepository,
    IJwtService jwtService) : IRequestHandler<LoginQuery, Result<string>>
{
    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByEmailAndPassword(request.Email, request.Password);

        if (user.IsFailed)
            return Result.Failure<string>(user.Error);

        var token = await jwtService.GenerateToken(user.Value.UserId, user.Value.Role.Code);

        return token;
    }
}
