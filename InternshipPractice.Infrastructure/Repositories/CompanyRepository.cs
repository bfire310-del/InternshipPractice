using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class CompanyRepository(InternshipPracticeDbContext dbContext): ICompanyRepository
{
    public async Task<Result<List<string?>>> GetCompanyNameList(string lang)
    {
        try
        {
            IQueryable<string?> query = lang switch
            {
                "kk" => dbContext.Companies.Select(vc => vc.CompanyNameKk),
                "ru" => dbContext.Companies.Select(vc => vc.CompanyNameRu),
                "en" => dbContext.Companies.Select(vc => vc.CompanyNameEn),
                _ => dbContext.Companies.Select(vc => vc.CompanyNameRu)
            };

            var nameList = await query.ToListAsync();

            return nameList;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<string?>>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении названий компаний: {ex.Message}"));
        }
    }

    public async Task<Result<int>> GetCompanyCount()
    {
        try
        {
            var count = await dbContext.Companies.CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества компаний: {ex.Message}"));
        }
    }
}
