using InternshipPractice.Application.Interfaces.HttpClients;
using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Interfaces.Services;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.Login;

public class LoginQueryHandler(IAccessHttpClient accessHttpClient) : IRequestHandler<LoginQuery, Result<(string, string, string, string)>>
{
    public async Task<Result<(string, string, string, string)>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await accessHttpClient.Login(request.Email, request.Password);

        if (result.IsFailed)
            return Result.Failure<(string,string, string, string)>(result.Error);

        return Result.Success((result.Value.Token, result.Value.RoleCode, result.Value.FirstName, result.Value.LastName));
    }
}
