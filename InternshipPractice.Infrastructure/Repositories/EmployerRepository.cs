using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class EmployerRepository(InternshipPracticeDbContext dbContext): IEmployerRepository
{
    public async Task<Result<int>> GetEmployerCount()
    {
        try
        {
            var count = await dbContext.Employers.CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества работадателей: {ex.Message}"));
        }
    }
}
