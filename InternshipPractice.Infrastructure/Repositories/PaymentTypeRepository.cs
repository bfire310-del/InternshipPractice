using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class PaymentTypeRepository(InternshipPracticeDbContext dbContext): IPaymentTypeRepository
{
    public async Task<Result<List<NameDto>>> GetPaymentTypeNameDtoList(string lang)
    {
        try
        {
            IQueryable<NameDto> query = lang switch
            {
                "kk" => dbContext.PaymentTypes
                    .Select(vc => new NameDto
                    {
                        Id = vc.PaymentTypeId,
                        Name = vc.NameKk
                    }),

                "ru" => dbContext.PaymentTypes
                    .Select(vc => new NameDto
                    {
                        Id = vc.PaymentTypeId,
                        Name = vc.NameRu
                    }),

                "en" => dbContext.PaymentTypes
                    .Select(vc => new NameDto
                    {
                        Id = vc.PaymentTypeId,
                        Name = vc.NameEn
                    }),

                _ => dbContext.PaymentTypes
                    .Select(vc => new NameDto
                    {
                        Id = vc.PaymentTypeId,
                        Name = vc.NameRu
                    })
            };

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<NameDto>>(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при получении названий типов оплаты: {ex.Message}"));
        }
    }

    public async Task<Result<List<PaymentTypeDto>>> GetAll()
    {
        try
        {
            return await dbContext.PaymentTypes
                .Select(p => new PaymentTypeDto
                {
                    PaymentTypeId = p.PaymentTypeId,
                    NameEn = p.NameEn,
                    NameRu = p.NameRu,
                    NameKk = p.NameKk
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<PaymentTypeDto>>(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    ex.Message));
        }
    }
}
