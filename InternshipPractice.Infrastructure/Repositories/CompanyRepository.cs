using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class CompanyRepository(InternshipPracticeDbContext dbContext): ICompanyRepository
{
    private ICompanyRepository _companyRepositoryImplementation;

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

    public async Task<Result<PagedResult<CompanySearchResponse>>> GetFilteredCompanyNamesAsync(
        string? query,
        Guid? regionId,
        Guid? categoryId,
        string lang,
        int page,
        int pageSize,
        Guid userId,
        CancellationToken ct)
    {
        var q = dbContext.Companies
            .AsNoTracking()
            .Where(c => c.DeletedAt == null);
        
        if (regionId is not null)
            q = q.Where(c => c.RegionId == regionId);

        if (categoryId is not null)
            q = q.Where(c => c.CompanyCategoryId == categoryId);

        if (!string.IsNullOrWhiteSpace(query))
        {
            var term = query.Trim();

            q = q.Where(c =>
                EF.Functions.ILike(c.CompanyNameRu!, $"%{term}%") ||
                EF.Functions.ILike(c.CompanyNameKk!, $"%{term}%") ||
                EF.Functions.ILike(c.CompanyNameEn!, $"%{term}%") ||
                EF.Functions.ILike(c.CompanyDescriptionRu!, $"%{term}%") ||
                EF.Functions.ILike(c.CompanyDescriptionKk!, $"%{term}%") ||
                EF.Functions.ILike(c.CompanyDescriptionEn!, $"%{term}%"));
        }
        
        page = page < 1 ? 1 : page;
        pageSize = pageSize is < 1 or > 100 ? 5 : pageSize;
        
        var totalCount = await q.CountAsync(ct);

        var companies = await q
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new
            {
                c.CompanyId,
                c.LinkToWebsite,
                
                c.CompanyNameRu,
                c.CompanyNameKk,
                c.CompanyNameEn,
                
                c.CompanyDescriptionRu,
                c.CompanyDescriptionKk,
                c.CompanyDescriptionEn,
                
                CategoryNameRu = c.CompanyCategory!.NameRu,
                CategoryNameKk = c.CompanyCategory.NameKk,
                CategoryNameEn = c.CompanyCategory.NameEn,
                
                RegionNameRu = c.Region!.NameRu,
                RegionNameKk = c.Region.NameKk,
                RegionNameEn = c.Region.NameEn,
                
                c.CreatedAt,
                VacancyCount = c.Employers
                    .SelectMany(e => e.Vacancies)
                    .Count(v => v.DeletedAt == null && v.Status != null && v.Status.Code == "active")
            })
            .ToListAsync(ct);

        var items = companies.Select(c => new CompanySearchResponse
            {
                CompanyId = c.CompanyId,
                
                Name = lang == "kk"
                    ? c.CompanyNameKk ?? c.CompanyNameRu
                    : lang == "en"
                        ? c.CompanyNameEn ?? c.CompanyNameRu
                        : c.CompanyNameRu,
                
                Website = c.LinkToWebsite,

                Description = lang == "kk"
                    ? c.CompanyDescriptionKk ?? c.CompanyDescriptionRu
                    : lang == "en"
                        ? c.CompanyDescriptionEn ?? c.CompanyDescriptionRu
                        : c.CompanyDescriptionRu,
                
                CategoryName = lang == "kk"
                    ? c.CategoryNameKk ?? c.CategoryNameRu
                    : lang == "en"
                        ? c.CategoryNameEn ?? c.CategoryNameRu
                        : c.CategoryNameRu,

                RegionName = lang == "kk"
                    ? c.RegionNameKk ?? c.RegionNameRu
                    : lang == "en"
                        ? c.RegionNameEn ?? c.RegionNameRu
                        : c.RegionNameRu,
                
                VacancyCount = c.VacancyCount,
                CreatedAt = c.CreatedAt
            }).ToList();
        
        return Result.Success(new PagedResult<CompanySearchResponse>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        });
    }
}
