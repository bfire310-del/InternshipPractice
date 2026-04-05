using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class RegionRepository(InternshipPracticeDbContext dbContext): IRegionRepository
{
    public async Task<Result<int>> GetRegionCount()
    {
        try
        {
            var count = await dbContext.Regions.CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества регионов: {ex.Message}"));
        }
    }
}
