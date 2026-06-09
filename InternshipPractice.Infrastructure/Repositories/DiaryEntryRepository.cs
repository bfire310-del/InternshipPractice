using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Entities;
using InternshipPractice.Domain.Requests;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class DiaryEntryRepository(InternshipPracticeDbContext dbContext): IDiaryEntryRepository
{
    public async Task<Result<List<DiaryEntryResponse>>> GetDiaryEntries(Guid userId)
    {
        try
        {
            var applicationId = await dbContext.Applications
                .Where(c =>
                    c.Student.UserId == userId &&
                    c.DeletedAt == null &&
                    c.DeletedAt == null &&
                    c.ApplicationStatus.Code == "contract_signed")
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => c.ApplicationId)
                .FirstOrDefaultAsync();

            if (applicationId == Guid.Empty)
            {
                return Result.Failure<List<DiaryEntryResponse>>(
                    new Error(Domain.Common.Error.NotFound, "Активная заявка не найдена"));
            }

            var entries = await dbContext.DiaryEntries
                .AsNoTracking()
                .Where(x =>
                    x.ApplicationId == applicationId &&
                    x.DeletedAt == null)
                .OrderByDescending(x => x.WorkDate)
                .Select(x => new DiaryEntryResponse
                {
                    DiaryEntryId = x.DiaryEntryId,
                    WorkDate = x.WorkDate,
                    Attendance = x.Attendance,
                    TaskName = x.TaskName,
                    Description = x.Description
                })
                .ToListAsync();

            return Result.Success(entries);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<DiaryEntryResponse>>(
                new Error(Domain.Common.Error.InternalServerError,
                    $"Ошибка при получении записей дневника: {ex.Message}"));
        }
    }
    
    public async Task<Result> CreateOrUpdateDiaryEntry(
        Guid userId,
        CreateDiaryEntryRequest request)
    {
        try
        {
            if (request.WorkDate == default)
            {
                return Result.Failure(
                    new Error(Domain.Common.Error.BadRequest, "Дата записи не передана"));
            }

            if (string.IsNullOrWhiteSpace(request.Attendance))
            {
                return Result.Failure(
                    new Error(Domain.Common.Error.BadRequest, "Посещаемость не передана"));
            }

            if (string.IsNullOrWhiteSpace(request.TaskName))
            {
                return Result.Failure(
                    new Error(Domain.Common.Error.BadRequest, "Название задачи не передано"));
            }

            var applicationId = await dbContext.Applications
                .Where(c =>
                    c.Student.UserId == userId &&
                    c.DeletedAt == null &&
                    c.DeletedAt == null &&
                    c.ApplicationStatus.Code == "contract_signed")
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => c.ApplicationId)
                .FirstOrDefaultAsync();

            if (applicationId == Guid.Empty)
            {
                return Result.Failure<List<DiaryEntryResponse>>(
                    new Error(Domain.Common.Error.NotFound, "Активная заявка не найдена"));
            }

            var entry = await dbContext.DiaryEntries
                .FirstOrDefaultAsync(x =>
                    x.ApplicationId == applicationId &&
                    x.WorkDate == request.WorkDate &&
                    x.DeletedAt == null);

            if (entry is null)
            {
                dbContext.DiaryEntries.Add(new DiaryEntry
                {
                    DiaryEntryId = Guid.NewGuid(),
                    ApplicationId = applicationId,
                    WorkDate = request.WorkDate,
                    Attendance = request.Attendance,
                    TaskName = request.TaskName,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId
                });
            }
            else
            {
                entry.Attendance = request.Attendance;
                entry.TaskName = request.TaskName;
                entry.Description = request.Description;
                entry.UpdatedAt = DateTime.UtcNow;
                entry.UpdatedBy = userId;
            }

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error(Domain.Common.Error.InternalServerError,
                    $"Ошибка при сохранении записи дневника: {ex.Message}"));
        }
    }
}
