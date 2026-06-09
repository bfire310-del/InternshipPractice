using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
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

    public async Task<Result<List<CompanyResponse>>> GetAll()
    {
        try
        {
            var all = await dbContext.Companies
                .Select(s => new CompanyResponse
                {
                    CompanyId = s.CompanyId,
                    UserId = s.UserId,
                    CompanyNameRu = s.CompanyNameRu,
                    CompanyNameKk = s.CompanyNameKk,
                    CompanyNameEn = s.CompanyNameEn,
                    CompanyDescriptionKk = s.CompanyDescriptionKk,
                    CompanyDescriptionEn = s.CompanyDescriptionEn,
                    CompanyDescriptionRu = s.CompanyDescriptionRu,
                    CompanyCategoryId = s.CompanyCategoryId,
                    LinkToWebsite = s.LinkToWebsite,
                    RegionId =s.RegionId
                })
                .ToListAsync();

            return all;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<CompanyResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<CompanyForCareerResponse>>> GetCompaniesForCareer()
    {
        try
        {
            return  await dbContext.Companies
            .Select(c => new CompanyForCareerResponse
            {
                CompanyId = c.CompanyId,

                CompanyName = c.CompanyNameRu,
                Description = c.CompanyDescriptionRu,

                RegionId = c.RegionId,
                RegionName = c.Region.NameRu,

                CompanyCategoryId = c.CompanyCategoryId,
                CompanyCategoryName = c.CompanyCategory.NameRu,

                VacanciesCount = dbContext.Vacancies
                    .Count(v => v.Employer.CompanyId == c.CompanyId),

                ContactFirstName = dbContext.Employers
                    .Where(e => e.CompanyId == c.CompanyId)
                    .Select(e => e.User.FirstName)
                    .FirstOrDefault(),

                ContactLastName = dbContext.Employers
                    .Where(e => e.CompanyId == c.CompanyId)
                    .Select(e => e.User.LastName)
                    .FirstOrDefault(),

                ContactEmail = dbContext.Employers
                    .Where(e => e.CompanyId == c.CompanyId)
                    .Select(e => e.User.Email)
                    .FirstOrDefault(),

                ContactPhone = dbContext.Employers
                    .Where(e => e.CompanyId == c.CompanyId)
                    .Select(e => e.User.PhoneNumber)
                    .FirstOrDefault(),
            })
            .ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<CompanyForCareerResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }
}
