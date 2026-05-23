using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Entities;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class UsersRepository(InternshipPracticeDbContext dbContext): IUsersRepository
{
    public async Task<Result<List<UserResponse>>> GetAll()
    {
        try
        {
            var result = await dbContext.Users
                .Select(u => new UserResponse
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Patronymic = u.Patronymic,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                })
                .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<UserResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<User>> GetUserByEmailAndPassword(string email, string password)
    {
        try
        {
            var result = await dbContext.Users
                .Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(password))
                .Include(u=>u.Role)
                .FirstAsync();
            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<User>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }
}
