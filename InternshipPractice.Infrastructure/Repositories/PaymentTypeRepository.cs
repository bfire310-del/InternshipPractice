using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
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
}
