using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Domain.Entities;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class PracticeFormRepository(InternshipPracticeDbContext dbContext): IPracticeFormRepository
{
    public async Task<Result<List<PracticeFormDto>>> GetAll()
    {
        try
        {
            return await dbContext.PracticeForms
                .Select(p => new PracticeFormDto
                {
                    PracticeFormId = p.PracticeFormId,
                    NameEn = p.NameEn,
                    NameKk = p.NameKk,
                    NameRu = p.NameRu,
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<PracticeFormDto>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }
}
