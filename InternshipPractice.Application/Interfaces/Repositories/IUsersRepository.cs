using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Entities;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Result<List<UserResponse>>> GetAll();
    Task<Result<User>> GetUserByEmailAndPassword(string email, string password);
}
