using InternshipPractice.Application.Helpers;
using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class ApplicationRepository(InternshipPracticeDbContext dbContext): IApplicationRepository
{
    public async Task<Result> CreateApplication(Guid userId, Guid vacancyId)
    {
        try
        {
            var studentId = await dbContext.Students
                .Where(s => s.UserId == userId && s.DeletedAt == null)
                .Select(s => s.StudentId)
                .FirstOrDefaultAsync();

            if (studentId == Guid.Empty)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Студент для текущего пользователя не найден"));
            }

            var vacancyExists = await dbContext.Vacancies
                .AnyAsync(v =>
                    v.VacancyId == vacancyId &&
                    v.DeletedAt == null &&
                    v.Status != null &&
                    v.Status.Code == "active");

            if (!vacancyExists)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Активная вакансия не найдена"));
            }

            var alreadyApplied = await dbContext.Applications
                .AnyAsync(a =>
                    a.StudentId == studentId &&
                    a.VacancyId == vacancyId &&
                    a.DeletedAt == null &&
                    a.ApplicationStatus.Code != "withdrawn");

            if (alreadyApplied)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.BadRequest,
                        "Вы уже откликнулись на эту вакансию"));
            }

            var submittedStatusId = await dbContext.ApplicationStatuses
                .Where(s => s.Code == "under_review" && s.DeletedAt == null)
                .Select(s => s.ApplicationStatusId)
                .FirstOrDefaultAsync();

            if (submittedStatusId == Guid.Empty)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.InternalServerError,
                        "Статус заявки under_review не найден"));
            }

            var application = new Domain.Entities.Application
            {
                ApplicationId = Guid.NewGuid(),
                StudentId = studentId,
                VacancyId = vacancyId,
                StatusId = submittedStatusId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId
            };

            dbContext.Applications.Add(application);

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    $"Ошибка при создании заявки: {ex.Message}"));
        }
    }
    
    public async Task<Result<PagedResult<ApplicationListResponse>>> GetApplicationsByStatus(
        Guid userId,
        string? statusCode,
        string lang,
        int page,
        int pageSize)
    {
        try
        {
            var studentId = await dbContext.Students
                .Where(s => s.UserId == userId && s.DeletedAt == null)
                .Select(s => s.StudentId)
                .FirstOrDefaultAsync();

            if (studentId == Guid.Empty)
            {
                return Result.Failure<PagedResult<ApplicationListResponse>>(
                    new Error(Domain.Common.Error.NotFound,
                        "Студент для текущего пользователя не найден"));
            }

            var q = dbContext.Applications
                .AsNoTracking()
                .Where(a => a.StudentId == studentId && a.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(statusCode))
                q = q.Where(a => a.ApplicationStatus.Code == statusCode);
            else
                q = q.Where(a => a.ApplicationStatus.Code != "withdrawn");

            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 5 : pageSize;

            var totalCount = await q.CountAsync();

            var applications = await q
                .OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new
                {
                    a.ApplicationId,
                    a.CreatedAt,
                    StatusNameRu = a.ApplicationStatus.NameRu,
                    StatusNameKk = a.ApplicationStatus.NameKk,
                    StatusNameEn = a.ApplicationStatus.NameEn,
                    VacancyId = a.Vacancy.VacancyId,
                    a.Vacancy.JobTitle,
                    a.Vacancy.ShortDescription,
                    a.Vacancy.Requirements,
                    a.Vacancy.StartDate,
                    a.Vacancy.EndDate,
                    CompanyNameRu = a.Vacancy.Employer.Company.CompanyNameRu,
                    CompanyNameKk = a.Vacancy.Employer.Company.CompanyNameKk,
                    CompanyNameEn = a.Vacancy.Employer.Company.CompanyNameEn,
                    CategoryNameRu = a.Vacancy.Category.NameRu,
                    CategoryNameKk = a.Vacancy.Category.NameKk,
                    CategoryNameEn = a.Vacancy.Category.NameEn,
                    WorkFormatNameRu = a.Vacancy.WorkFormat.NameRu,
                    WorkFormatNameKk = a.Vacancy.WorkFormat.NameKk,
                    WorkFormatNameEn = a.Vacancy.WorkFormat.NameEn,
                    RegionNameRu = a.Vacancy.Region.NameRu,
                    RegionNameKk = a.Vacancy.Region.NameKk,
                    RegionNameEn = a.Vacancy.Region.NameEn,
                    PaymentTypeNameRu = a.Vacancy.PaymentType.NameRu,
                    PaymentTypeNameKk = a.Vacancy.PaymentType.NameKk,
                    PaymentTypeNameEn = a.Vacancy.PaymentType.NameEn,
                    TypeOfEmploymentNameRu = a.Vacancy.TypeOfEmployment.NameRu,
                    TypeOfEmploymentNameKk = a.Vacancy.TypeOfEmployment.NameKk,
                    TypeOfEmploymentNameEn = a.Vacancy.TypeOfEmployment.NameEn,
                    StatusCode = a.ApplicationStatus.Code
                })
                .ToListAsync();

            var items = applications.Select(a => new ApplicationListResponse
            {
                ApplicationId = a.ApplicationId,
                VacancyId = a.VacancyId,
                JobTitle = a.JobTitle ?? "",
                Status = lang == "kk" ? a.StatusNameKk ?? a.StatusNameRu : lang == "en" ? a.StatusNameEn ?? a.StatusNameRu : a.StatusNameRu,
                StatusCode = a.StatusCode,
                CompanyName = lang == "kk" ? a.CompanyNameKk ?? a.CompanyNameRu : lang == "en" ? a.CompanyNameEn ?? a.CompanyNameRu : a.CompanyNameRu,
                CategoryName = lang == "kk" ? a.CategoryNameKk ?? a.CategoryNameRu : lang == "en" ? a.CategoryNameEn ?? a.CategoryNameRu : a.CategoryNameRu,
                WorkFormatName = lang == "kk" ? a.WorkFormatNameKk ?? a.WorkFormatNameRu : lang == "en" ? a.WorkFormatNameEn ?? a.WorkFormatNameRu : a.WorkFormatNameRu,
                ShortDescription = a.ShortDescription,
                Requirements = a.Requirements,
                RegionName = lang == "kk" ? a.RegionNameKk ?? a.RegionNameRu : lang == "en" ? a.RegionNameEn ?? a.RegionNameRu : a.RegionNameRu,
                Duration = DurationHelper.CalculateDurationText(a.StartDate, a.EndDate),
                PaymentType = lang == "kk" ? a.PaymentTypeNameKk ?? a.PaymentTypeNameRu : lang == "en" ? a.PaymentTypeNameEn ?? a.PaymentTypeNameRu : a.PaymentTypeNameRu,
                TypeOfEmployment = lang == "kk" ? a.TypeOfEmploymentNameKk ?? a.TypeOfEmploymentNameRu : lang == "en" ? a.TypeOfEmploymentNameEn ?? a.TypeOfEmploymentNameRu : a.TypeOfEmploymentNameRu,
                CreatedAt = a.CreatedAt,
                CanWithdraw =
                    a.StatusCode == "under_review" ||
                    a.StatusCode == "approved",
                CanSignContract =
                    a.StatusCode == "approved"
            }).ToList();

            return new PagedResult<ApplicationListResponse>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<ApplicationListResponse>>(
                new Error(Domain.Common.Error.InternalServerError,
                    $"Ошибка при получении заявок: {ex.Message}"));
        }
    }
    
    public async Task<Result> WithdrawApplication(Guid userId, Guid applicationId)
    {
        try
        {
            var studentId = await dbContext.Students
                .Where(s => s.UserId == userId && s.DeletedAt == null)
                .Select(s => s.StudentId)
                .FirstOrDefaultAsync();

            if (studentId == Guid.Empty)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Студент для текущего пользователя не найден"));
            }

            var application = await dbContext.Applications
                .Include(a => a.ApplicationStatus)
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId == applicationId &&
                    a.StudentId == studentId &&
                    a.DeletedAt == null);

            if (application is null)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Заявка не найдена"));
            }

            var currentStatusCode = application.ApplicationStatus.Code;

            if (currentStatusCode is not ("under_review" or "approved"))
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.BadRequest,
                        "Заявку можно отозвать только если она на рассмотрении или одобрена"));
            }

            var withdrawnStatusId = await dbContext.ApplicationStatuses
                .Where(s => s.Code == "withdrawn" && s.DeletedAt == null)
                .Select(s => s.ApplicationStatusId)
                .FirstOrDefaultAsync();

            if (withdrawnStatusId == Guid.Empty)
            {
                return Result.Failure(
                    new Error(
                        Domain.Common.Error.InternalServerError,
                        "Статус заявки withdrawn не найден"));
            }

            application.StatusId = withdrawnStatusId;
            application.UpdatedAt = DateTime.UtcNow;
            application.UpdatedBy = userId;

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    $"Ошибка при отзыве заявки: {ex.Message}"));
        }
    }

    public async Task<Result> ApproveApplicationWithoutSave(Guid userId, Guid applicationId)
    {
        try
        {
            var application = await dbContext.Applications
                .Include(a => a.ApplicationStatus)
                .Include(a => a.Vacancy)
                    .ThenInclude(v => v.Employer)
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId == applicationId &&
                    a.DeletedAt == null);

            if (application is null)
            {
                return Result.Failure(new Error(Domain.Common.Error.NotFound, "Заявка не найдена"));
            }

            if (application.Vacancy?.Employer?.UserId != userId)
            {
                return Result.Failure(new Error(Domain.Common.Error.Forbidden, "Вы не можете изменить статус заявки на чужую вакансию"));
            }

            if (application.ApplicationStatus.Code != "under_review")
            {
                return Result.Failure(new Error(Domain.Common.Error.BadRequest, "Заявку можно одобрить только если она находится на рассмотрении"));
            }

            var newStatusId = await dbContext.ApplicationStatuses
                .Where(s => s.Code == "approved" && s.DeletedAt == null)
                .Select(s => s.ApplicationStatusId)
                .FirstOrDefaultAsync();

            if (newStatusId == Guid.Empty)
            {
                return Result.Failure(new Error(Domain.Common.Error.InternalServerError, $"Статус заявки approved не найден"));
            }

            application.StatusId = newStatusId;
            application.UpdatedAt = DateTime.UtcNow;
            application.UpdatedBy = userId;
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    $"Ошибка при изменении статуса заявки: {ex.Message}"));
        }
    }

    public async Task<Result> RejectApplication(Guid userId, Guid applicationId)
    {
        try
        {
            var application = await dbContext.Applications
                .Include(a => a.ApplicationStatus)
                .Include(a => a.Vacancy)
                .ThenInclude(v => v.Employer)
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId == applicationId &&
                    a.DeletedAt == null);

            if (application is null)
            {
                return Result.Failure(new Error(Domain.Common.Error.NotFound, "Заявка не найдена"));
            }

            if (application.Vacancy?.Employer?.UserId != userId)
            {
                return Result.Failure(new Error(Domain.Common.Error.Forbidden, "Вы не можете изменить статус заявки на чужую вакансию"));
            }

            if (application.ApplicationStatus.Code != "under_review")
            {
                return Result.Failure(new Error(Domain.Common.Error.BadRequest, "Заявку можно одобрить только если она находится на рассмотрении"));
            }

            var newStatusId = await dbContext.ApplicationStatuses
                .Where(s => s.Code == "rejected" && s.DeletedAt == null)
                .Select(s => s.ApplicationStatusId)
                .FirstOrDefaultAsync();

            if (newStatusId == Guid.Empty)
            {
                return Result.Failure(new Error(Domain.Common.Error.InternalServerError, $"Статус заявки rejected не найден"));
            }

            application.StatusId = newStatusId;
            application.UpdatedAt = DateTime.UtcNow;
            application.UpdatedBy = userId;

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    $"Ошибка при изменении статуса заявки: {ex.Message}"));
        }
    }
    public async Task<Result<int>> GetResponsesCountByEmployerId(Guid userId)
    {
        try
        {
            var result = await dbContext.Applications
            .Join(
                dbContext.Vacancies,
                application => application.VacancyId,
                vacancy => vacancy.VacancyId,
                (application, vacancy) => new { application, vacancy }
            )
            .Join(
                dbContext.Employers,
                x => x.vacancy.EmployerId,
                employer => employer.EmployerId,
                (x, employer) => new { x.application, x.vacancy, employer }
            )
            .Where(x => x.employer.UserId == userId)
            .CountAsync();
            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении количества работадателей: {ex.Message}"));
        }
    }
    public async Task<Result<int>> GetCongratsContracts(Guid userId)
    {
        try
        {
            var result = await dbContext.Applications
            .Join(
                dbContext.Vacancies,
                application => application.VacancyId,
                vacancy => vacancy.VacancyId,
                (application, vacancy) => new { application, vacancy }
            )
            .Join(
                dbContext.Employers,
                x => x.vacancy.EmployerId,
                employer => employer.EmployerId,
                (x, employer) => new { x.application, x.vacancy, employer }
            )
            .Where(x => x.employer.UserId == userId
            && x.application.StatusId == Guid.Parse("ddae979f-b41d-4580-ac91-afa62b19f231"))
            .CountAsync();

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении количества работадателей: {ex.Message}"));
        }
    }

}
