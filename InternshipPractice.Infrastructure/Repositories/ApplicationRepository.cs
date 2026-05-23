using InternshipPractice.Application.Interfaces.Repositories;
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
                    a.DeletedAt == null);

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
}
