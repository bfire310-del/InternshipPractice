using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class CompanyCategoryRepository(InternshipPracticeDbContext dbContext): ICompanyCategoryRepository
{
    public async Task<Result<List<NameDto>>> GetCompanyCategoryNameDtoList(string lang)
    {
        try
        {
            IQueryable<NameDto> query = lang switch
            {
                "kk" => dbContext.CompanyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.CompanyCategoryId,
                        Name = vc.NameKk
                    }),

                "ru" => dbContext.CompanyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.CompanyCategoryId,
                        Name = vc.NameRu
                    }),

                "en" => dbContext.CompanyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.CompanyCategoryId,
                        Name = vc.NameEn
                    }),

                _ => dbContext.CompanyCategories
                    .Select(vc => new NameDto
                    {
                        Id = vc.CompanyCategoryId,
                        Name = vc.NameRu
                    })
            };

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<NameDto>>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении названий категорий компаний: {ex.Message}"));
        }
    }
}
