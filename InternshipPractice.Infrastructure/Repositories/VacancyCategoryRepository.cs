using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class VacancyCategoryRepository(InternshipPracticeDbContext dbContext): IVacancyCategoryRepository
{
    public async Task<Result<List<NameDto>>> GetVacancyCategoryNameDtoList(string lang)
    {
        try
        {
            IQueryable<NameDto> query = lang switch
            {
                "kk" => dbContext.VacancyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.VacancyCategoryId,
                        Name = vc.NameKk
                    }),

                "ru" => dbContext.VacancyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.VacancyCategoryId,
                        Name = vc.NameRu
                    }),

                "en" => dbContext.VacancyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.VacancyCategoryId,
                        Name = vc.NameEn
                    }),

                _ => dbContext.VacancyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.VacancyCategoryId,
                        Name = vc.NameRu
                    })
            };

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<NameDto>>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении названий категорий практик: {ex.Message}"));
        }
    }

    public async Task<Result<List<VacancyCategoryDto>>> GetAll()
    {
        try
        {
            return await dbContext.VacancyCategories
                .Select(v => new VacancyCategoryDto
                {
                    VacancyCategoryId = v.VacancyCategoryId,
                    NameEn = v.NameEn,
                    NameRu = v.NameRu,
                    NameKk = v.NameKk,
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<VacancyCategoryDto>>(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    ex.Message));
        }
    }
}
