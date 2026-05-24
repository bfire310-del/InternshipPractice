using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class WorkFormatRepository(InternshipPracticeDbContext dbContext): IWorkFormatRepository
{
    public async Task<Result<List<WorkFormatDto>>> GetAll()
    {
        try
        {
            return Result.Success(await dbContext.WorkFormats
                .Select(w => new WorkFormatDto
                {
                    WorkFormatId = w.WorkFormatId,
                    NameEn = w.NameEn,
                    NameKk = w.NameKk,
                    NameRu = w.NameRu,
                })
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return Result.Failure<List<WorkFormatDto>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }
}
