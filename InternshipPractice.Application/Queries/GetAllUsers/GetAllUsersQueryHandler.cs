using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUsersRepository usersRepository) : IRequestHandler<GetAllUsersQuery, Result<List<UserResponse>>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await usersRepository.GetAll();
        return result;
    }
}
