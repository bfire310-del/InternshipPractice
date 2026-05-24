using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IPaymentTypeRepository
{
    Task<Result<List<NameDto>>> GetPaymentTypeNameDtoList(string lang);
    Task<Result<List<PaymentTypeDto>>> GetAll();
}
