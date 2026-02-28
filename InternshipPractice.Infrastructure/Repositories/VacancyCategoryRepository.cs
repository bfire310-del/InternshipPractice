using InternshipPractice.Domain.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class VacancyCategoryRepository(InternshipPracticeDbContext _dbContext): IVacancyCategoryRepository
{
    public async Task<Result<List<string?>>> GetVacancyCategoryNameList(string lang)
    {
        try
        {
            IQueryable<string?> query = lang switch
            {
                "kk" => _dbContext.VacancyCategories.Select(vc => vc.NameKk),
                "ru" => _dbContext.VacancyCategories.Select(vc => vc.NameRu),
                "en" => _dbContext.VacancyCategories.Select(vc => vc.NameEn),
                _ => _dbContext.VacancyCategories.Select(vc => vc.NameRu)
            };

            var nameList = await query.ToListAsync();

            return nameList;
        }
            catch (Exception ex)
        {
            return Result.Failure<List<string?>>(new Error(Domain.Common.Error.InternalServerError,"Ошибка при получении названии категории практик"));
        }
    }
}
