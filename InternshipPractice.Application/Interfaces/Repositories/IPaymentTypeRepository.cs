using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IPaymentTypeRepository
{
    Task<Result<List<NameDto>>> GetPaymentTypeNameDtoList(string lang);
}
