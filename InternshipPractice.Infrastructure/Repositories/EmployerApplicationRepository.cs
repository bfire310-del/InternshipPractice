using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class EmployerApplicationRepository(InternshipPracticeDbContext dbContext) : IEmployerApplicationRepository
{
    public async Task<Result<PagedResult<EmployerApplicationResponse>>> GetEmployerApplications(
        Guid userId,
        string? statusCode,
        string lang,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        try
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;
            lang = string.IsNullOrWhiteSpace(lang) ? "ru" : lang.ToLowerInvariant();

            var q = dbContext.Applications
                .AsNoTracking()
                .Where(a =>
                    a.DeletedAt == null &&
                    a.Vacancy != null &&
                    a.Vacancy.DeletedAt == null &&
                    a.Vacancy.Employer != null &&
                    a.Vacancy.Employer.UserId == userId);

            if (!string.IsNullOrWhiteSpace(statusCode))
            {
                q = q.Where(a => a.ApplicationStatus.Code == statusCode);
            }
            else
            {
                q = q.Where(a => a.ApplicationStatus.Code != "withdrawn");
            }

            var totalCount = await q.CountAsync(cancellationToken);

            var rows = await q
                .OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new
                {
                    a.ApplicationId,
                    ContractId = dbContext.Contracts.FirstOrDefault(x => x.ApplicationId == a.ApplicationId) == null ? Guid.Empty : dbContext.Contracts.FirstOrDefault(x => x.ApplicationId == a.ApplicationId)!.ContractId ,
                    a.VacancyId,
                    a.StudentId,
                    StudentUserId = (Guid?)a.Student.UserId,
                    a.Student.User.FirstName,
                    a.Student.User.LastName,
                    a.Student.User.Patronymic,
                    StudentEmail = a.Student.User.Email,
                    StudentPhone = a.Student.User.PhoneNumber,
                    Course = (int?)a.Student.Course,
                    a.Student.Gpa,
                    FacultyNameRu = a.Student.Faculty.NameRu,
                    FacultyNameKk = a.Student.Faculty.NameKk,
                    FacultyNameEn = a.Student.Faculty.NameEn,
                    StudentStatusRu = a.Student.Status.NameRu,
                    StudentStatusKk = a.Student.Status.NameKk,
                    StudentStatusEn = a.Student.Status.NameEn,
                    Skills = a.Student.StudentSkillMaps
                        .Where(m => m.DeletedAt == null)
                        .Select(m => new
                        {
                            m.Skill.NameRu,
                            m.Skill.NameKk,
                            m.Skill.NameEn
                        })
                        .ToList(),
                    a.Vacancy.JobTitle,
                    VacancyNameRu = a.Vacancy.NameRu,
                    VacancyNameKk = a.Vacancy.NameKk,
                    VacancyNameEn = a.Vacancy.NameEn,
                    StatusNameRu = a.ApplicationStatus.NameRu,
                    StatusNameKk = a.ApplicationStatus.NameKk,
                    StatusNameEn = a.ApplicationStatus.NameEn,
                    StatusCode = a.ApplicationStatus.Code,
                    a.CreatedAt
                })
                .ToListAsync(cancellationToken);

            var items = rows.Select(a => new EmployerApplicationResponse
            {
                ApplicationId = a.ApplicationId,
                ContractId = a.ContractId,
                VacancyId = a.VacancyId,
                StudentId = a.StudentId,
                StudentUserId = a.StudentUserId,
                StudentFullName = string.Join(" ", new[] { a.LastName, a.FirstName, a.Patronymic }
                    .Where(x => !string.IsNullOrWhiteSpace(x))),
                StudentEmail = a.StudentEmail,
                StudentPhone = a.StudentPhone,
                Course = a.Course,
                Gpa = a.Gpa,
                FacultyName = lang == "kk"
                    ? a.FacultyNameKk ?? a.FacultyNameRu
                    : lang == "en"
                        ? a.FacultyNameEn ?? a.FacultyNameRu
                        : a.FacultyNameRu,
                StudentStatus = lang == "kk"
                    ? a.StudentStatusKk ?? a.StudentStatusRu
                    : lang == "en"
                        ? a.StudentStatusEn ?? a.StudentStatusRu
                        : a.StudentStatusRu,
                Skills = a.Skills
                    .Select(s => lang == "kk" ? s.NameKk ?? s.NameRu : lang == "en" ? s.NameEn ?? s.NameRu : s.NameRu)
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList(),
                JobTitle = a.JobTitle ?? a.VacancyNameRu ?? "Вакансия",
                VacancyName = lang == "kk"
                    ? a.VacancyNameKk ?? a.VacancyNameRu
                    : lang == "en"
                        ? a.VacancyNameEn ?? a.VacancyNameRu
                        : a.VacancyNameRu,
                Status = lang == "kk"
                    ? a.StatusNameKk ?? a.StatusNameRu
                    : lang == "en"
                        ? a.StatusNameEn ?? a.StatusNameRu
                        : a.StatusNameRu,
                StatusCode = a.StatusCode,
                CreatedAt = a.CreatedAt
            }).ToList();

            return new PagedResult<EmployerApplicationResponse>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<EmployerApplicationResponse>>(
                new Error(Domain.Common.Error.InternalServerError,
                    $"Ошибка при получении откликов работодателя: {ex.Message}"));
        }
    }
}