using InternshipPractice.Application.Helpers;
using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
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

    public async Task<Result<PagedResult<VacancySearchResponse>>> GetFilteredVacancyNamesAsync(
        string? query,
        Guid? regionId,
        Guid? paymentTypeId,
        string? durationCode,
        Guid? categoryId,
        string lang,
        int page,
        int pageSize,
        CancellationToken ct)
    {
        try
        {
            var q = dbContext.Vacancies
                .AsNoTracking()
                .Where(v => v.DeletedAt == null && v.Status != null && v.Status.Code == "active");

            if (regionId is not null)
                q = q.Where(v => v.RegionId == regionId);

            if (categoryId is not null)
                q = q.Where(v => v.CategoryId == categoryId);

            if (paymentTypeId is not null)
                q = q.Where(v => v.PaymentTypeId == paymentTypeId);

            if (!string.IsNullOrWhiteSpace(durationCode))
            {
                q = durationCode switch
                {
                    "up_to_1_month" => q.Where(v =>
                        v.StartDate != null &&
                        v.EndDate != null &&
                        v.EndDate <= v.StartDate.Value.AddMonths(1)),

                    "1_2_months" => q.Where(v =>
                        v.StartDate != null &&
                        v.EndDate != null &&
                        v.EndDate > v.StartDate.Value.AddMonths(1) &&
                        v.EndDate <= v.StartDate.Value.AddMonths(2)),

                    "2_3_months" => q.Where(v =>
                        v.StartDate != null &&
                        v.EndDate != null &&
                        v.EndDate > v.StartDate.Value.AddMonths(2) &&
                        v.EndDate <= v.StartDate.Value.AddMonths(3)),

                    "more_than_3_months" => q.Where(v =>
                        v.StartDate != null &&
                        v.EndDate != null &&
                        v.EndDate > v.StartDate.Value.AddMonths(3)),

                    _ => q
                };
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = query.Trim();

                q = q.Where(v =>
                    EF.Functions.ILike(v.NameRu!, $"%{term}%") ||
                    EF.Functions.ILike(v.NameKk!, $"%{term}%") ||
                    EF.Functions.ILike(v.NameEn!, $"%{term}%") ||
                    EF.Functions.ILike(v.ShortDescription!, $"%{term}%") ||
                    EF.Functions.ILike(v.Requirements!, $"%{term}%") ||
                    EF.Functions.ILike(v.JobTitle!, $"%{term}%"));
            }

            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 5 : pageSize;

            var totalCount = await q.CountAsync(ct);

            var vacancies = await q
                .OrderByDescending(v => v.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(v => new
                {
                    v.VacancyId,
                    v.JobTitle,
                    v.ShortDescription,
                    v.Requirements,
                    v.StartDate,
                    v.EndDate,
                    v.CreatedAt,

                    CompanyNameRu = v.Employer!.Company!.CompanyNameRu,
                    CompanyNameKk = v.Employer.Company.CompanyNameKk,
                    CompanyNameEn = v.Employer.Company.CompanyNameEn,

                    CategoryNameRu = v.Category!.NameRu,
                    CategoryNameKk = v.Category.NameKk,
                    CategoryNameEn = v.Category.NameEn,

                    WorkFormatNameRu = v.WorkFormat!.NameRu,
                    WorkFormatNameKk = v.WorkFormat.NameKk,
                    WorkFormatNameEn = v.WorkFormat.NameEn,

                    RegionNameRu = v.Region!.NameRu,
                    RegionNameKk = v.Region.NameKk,
                    RegionNameEn = v.Region.NameEn,

                    PaymentTypeNameRu = v.PaymentType!.NameRu,
                    PaymentTypeNameKk = v.PaymentType.NameKk,
                    PaymentTypeNameEn = v.PaymentType.NameEn,

                    TypeOfEmploymentNameRu = v.TypeOfEmployment!.NameRu,
                    TypeOfEmploymentNameKk = v.TypeOfEmployment.NameKk,
                    TypeOfEmploymentNameEn = v.TypeOfEmployment.NameEn,

                    StatusNameRu = v.Status!.NameRu,
                    StatusNameKk = v.Status.NameKk,
                    StatusNameEn = v.Status.NameEn
                })
                .ToListAsync(ct);

            var items = vacancies.Select(v => new VacancySearchResponse
            {
                VacancyId = v.VacancyId,
                JobTitle = v.JobTitle ?? "",

                CompanyName = lang == "kk"
                    ? v.CompanyNameKk ?? v.CompanyNameRu
                    : lang == "en"
                        ? v.CompanyNameEn ?? v.CompanyNameRu
                        : v.CompanyNameRu,

                CategoryName = lang == "kk"
                    ? v.CategoryNameKk ?? v.CategoryNameRu
                    : lang == "en"
                        ? v.CategoryNameEn ?? v.CategoryNameRu
                        : v.CategoryNameRu,

                WorkFormatName = lang == "kk"
                    ? v.WorkFormatNameKk ?? v.WorkFormatNameRu
                    : lang == "en"
                        ? v.WorkFormatNameEn ?? v.WorkFormatNameRu
                        : v.WorkFormatNameRu,

                ShortDescription = v.ShortDescription,
                Requirements = v.Requirements,

                RegionName = lang == "kk"
                    ? v.RegionNameKk ?? v.RegionNameRu
                    : lang == "en"
                        ? v.RegionNameEn ?? v.RegionNameRu
                        : v.RegionNameRu,

                Duration = DurationHelper.CalculateDurationText(v.StartDate, v.EndDate),

                PaymentType = lang == "kk"
                    ? v.PaymentTypeNameKk ?? v.PaymentTypeNameRu
                    : lang == "en"
                        ? v.PaymentTypeNameEn ?? v.PaymentTypeNameRu
                        : v.PaymentTypeNameRu,

                TypeOfEmployment = lang == "kk"
                    ? v.TypeOfEmploymentNameKk ?? v.TypeOfEmploymentNameRu
                    : lang == "en"
                        ? v.TypeOfEmploymentNameEn ?? v.TypeOfEmploymentNameRu
                        : v.TypeOfEmploymentNameRu,

                Status = lang == "kk"
                    ? v.StatusNameKk ?? v.StatusNameRu
                    : lang == "en"
                        ? v.StatusNameEn ?? v.StatusNameRu
                        : v.StatusNameRu,

                CreatedAt = v.CreatedAt
            }).ToList();

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
                    JobTitle = s.JobTitle,
                    ShortDescription = s.ShortDescription,
                    RegionName = s.Region.NameRu,
                    
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
                    JobTitle = s.JobTitle,
                    ShortDescription = s.ShortDescription,
                    RegionName = s.Region.NameRu,
                    
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

    public async Task<Result<VacancyDetailResponse>> GetVacancyDetailsById(Guid id)
    {
        try
        {
            var vacancy = await dbContext.Vacancies
                .AsNoTracking()
                .Where(v =>
                    v.DeletedAt == null &&
                    v.Status != null &&
                    v.Status.Code == "active" &&
                    v.VacancyId == id)
                .Select(v => new VacancyDetailResponse
                {
                    VacancyId = v.VacancyId,

                    JobTitle = v.JobTitle ?? "",

                    Status = v.Status != null
                        ? v.Status.NameRu
                        : null,

                    CompanyName = v.Employer != null && v.Employer.Company != null
                        ? v.Employer.Company.CompanyNameRu
                        : null,

                    CategoryName = v.Category != null
                        ? v.Category.NameRu
                        : null,

                    WorkFormatName = v.WorkFormat != null
                        ? v.WorkFormat.NameRu
                        : null,

                    ShortDescription = v.ShortDescription,
                    
                    FullDescription = v.FullDescription,

                    Requirements = v.Requirements,

                    RegionName = v.Region != null
                        ? v.Region.NameRu
                        : null,

                    Duration = DurationHelper.CalculateDurationText(v.StartDate, v.EndDate),

                    PaymentType = v.PaymentType != null
                        ? v.PaymentType.NameRu
                        : null,

                    TypeOfEmployment = v.TypeOfEmployment != null
                        ? v.TypeOfEmployment.NameRu
                        : null,
                    
                    CreatedAt = v.CreatedAt,
                    
                    Skills = v.VacancySkillMaps
                        .Where(x => x.DeletedAt == null)
                        .Select(x => x.Skill.NameRu!)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (vacancy is null)
            {
                return Result.Failure<VacancyDetailResponse>(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Вакансия не найдена"));
            }

            return vacancy;
        }
        catch (Exception ex)
        {
            return Result.Failure<VacancyDetailResponse>(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    ex.Message));
        }
    }
}
