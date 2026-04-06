using InternshipPractice.Api.Responses;
using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class VacancyRepository(InternshipPracticeDbContext dbContext): IVacancyRepository
{
    public async Task<Result<int>> GetVacancyCount()
    {
        try
        {
            var count = await dbContext.Vacancies.CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества вакансий: {ex.Message}"));
        }
    }

    public async Task<Result<int>> GetVacancyCountNew()
    {
        try
        {
            var count = await dbContext.Vacancies.CountAsync(x => x.CreatedAt >= DateTime.UtcNow.AddMonths(-1));
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении количества новых вакансий: {ex.Message}"));
        }
    }

    public async Task<Result<PagedResult<VacancySearchResponse>>> GetFilteredVacancyNamesAsync(string? query, Guid? regionId, Guid? categoryId, Guid? workFormatId,
        Guid? practiceFormId, Guid? typeOfEmploymentId, int? course, bool onlyPublished, bool? onlyPaid,
        int? durationMonthsMin, int? durationMonthsMax, string lang, int page, int pageSize, CancellationToken ct)
    {
        try
    {
        var q = dbContext.Vacancies
            .AsNoTracking()
            .Where(v => v.DeletedAt == null);

        if (regionId is not null) q = q.Where(v => v.RegionId == regionId);
        if (categoryId is not null) q = q.Where(v => v.CategoryId == categoryId);
        if (workFormatId is not null) q = q.Where(v => v.WorkFormatId == workFormatId);
        if (practiceFormId is not null) q = q.Where(v => v.PracticeFormId == practiceFormId);
        if (typeOfEmploymentId is not null) q = q.Where(v => v.TypeOfEmploymentId == typeOfEmploymentId);
        if (course is not null) q = q.Where(v => v.Course == course);
        if (onlyPaid == true) q = q.Where(v => v.Payment != null && v.Payment != "");

        if (!string.IsNullOrWhiteSpace(query))
        {
            var term = query.Trim();
            q = q.Where(v =>
                EF.Functions.ILike(v.NameRu!, $"%{term}%") ||
                EF.Functions.ILike(v.NameKk!, $"%{term}%") ||
                EF.Functions.ILike(v.NameEn!, $"%{term}%"));
        }

        var totalCount = await q.CountAsync(ct);

        page = page < 1 ? 1 : page;
        pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

        var items = await q
            .OrderByDescending(v => v.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VacancySearchResponse
            {
                VacancyId = v.VacancyId,
                Title = lang == "kk" ? (v.NameKk ?? v.NameRu)
                      : lang == "en" ? (v.NameEn ?? v.NameRu)
                      : v.NameRu,
                Payment = v.Payment,
                CreatedAt = v.CreatedAt
            })
            .ToListAsync(ct);

        var result = new PagedResult<VacancySearchResponse>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return result;
    }
    catch (Exception ex)
    {
        return Result.Failure<PagedResult<VacancySearchResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
    }
    }

    public async Task<Result<List<VacancySearchResponse>>> GetVacancyByLikeWord(string word)
    {
        try
        {
            var result = await dbContext.Vacancies
                .Where(v => v.NameEn.Contains(word)
                || v.NameRu.Contains(word)
                || v.NameKk.Contains(word))
                .Select(s => new VacancySearchResponse
                {
                    Title = s.JobTitle,
                    ShortDescription = s.ShortDescription,
                    RegionName = s.Region.NameRu,
                    Payment = s.Payment,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();

            return result;
        }
        catch(Exception ex)
        {
            return Result.Failure<List<VacancySearchResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<int>> GetActiveVacanciesCountByCompanyId(Guid companyId)
    {
        try
        {
            var count = await dbContext.Companies
                .Where(c => c.CompanyId == companyId)
                .Select(s => s.Region.Vacancies)
                .CountAsync();

            return count;    
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<VacancySearchResponse>>> GetVacanciesByEmployerId(Guid employerId)
    {
        try
        {
            var result = await dbContext.Vacancies
                .Where(v=> v.EmployerId == employerId)
                .Select(s => new VacancySearchResponse
                {
                    Title = s.JobTitle,
                    ShortDescription = s.ShortDescription,
                    RegionName = s.Region.NameRu,
                    Payment = s.Payment,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<VacancySearchResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }
}
