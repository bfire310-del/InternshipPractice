using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Result<List<UserResponse>>> GetAll();
}
