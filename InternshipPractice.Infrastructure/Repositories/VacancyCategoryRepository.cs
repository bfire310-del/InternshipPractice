using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class VacancyCategoryRepository(InternshipPracticeDbContext dbContext): IVacancyCategoryRepository
{
    public async Task<Result<List<string?>>> GetVacancyCategoryNameList(string lang)
    {
        try
        {
            IQueryable<string?> query = lang switch
            {
                "kk" => dbContext.VacancyCategories.Select(vc => vc.NameKk),
                "ru" => dbContext.VacancyCategories.Select(vc => vc.NameRu),
                "en" => dbContext.VacancyCategories.Select(vc => vc.NameEn),
                _ => dbContext.VacancyCategories.Select(vc => vc.NameRu)
            };
            
            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<string?>>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении названий категорий практик: {ex.Message}"));
        }
    }
}
