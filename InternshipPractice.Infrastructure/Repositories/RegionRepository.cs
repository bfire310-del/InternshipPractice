using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
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

    public async Task<Result<List<NameDto>>> GetRegionNameDtoList(string lang)
    {
        try
        {
            IQueryable<NameDto> query = lang switch
            {
                "kk" => dbContext.Regions
                    .Select(vc => new NameDto
                    {
                        Id = vc.RegionId,
                        Name = vc.NameKk
                    }),

                "ru" => dbContext.Regions
                    .Select(vc => new NameDto
                    {
                        Id = vc.RegionId,
                        Name = vc.NameRu
                    }),

                "en" => dbContext.Regions
                    .Select(vc => new NameDto
                    {
                        Id = vc.RegionId,
                        Name = vc.NameEn
                    }),

                _ => dbContext.Regions
                    .Select(vc => new NameDto
                    {
                        Id = vc.RegionId,
                        Name = vc.NameRu
                    })
            };

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<NameDto>>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении названий регионов: {ex.Message}"));
        }
    }
}
