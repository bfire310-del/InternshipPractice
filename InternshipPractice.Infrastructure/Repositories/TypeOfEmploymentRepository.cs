using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class TypeOfEmploymentRepository(InternshipPracticeDbContext dbContext): ITypeOfEmploymentRepository
{
    public async Task<Result<List<TypeOfEmploymentDto>>> GetAll()
    {
        try
        {
            return await dbContext.TypeOfEmployments
                .Select(t => new TypeOfEmploymentDto
                {
                    TypeOfEmploymentId = t.TypeOfEmploymentId,
                    NameEn = t.NameEn,
                    NameKk = t.NameKk,
                    NameRu = t.NameRu,
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<TypeOfEmploymentDto>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }
}
