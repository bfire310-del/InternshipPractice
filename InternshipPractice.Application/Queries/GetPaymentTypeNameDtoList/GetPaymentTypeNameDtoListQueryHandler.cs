using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetPaymentTypeNameDtoList;

public class GetPaymentTypeNameDtoListQueryHandler(IPaymentTypeRepository paymentTypeRepository) : IRequestHandler<GetPaymentTypeNameDtoListQuery, Result<List<NameDto>>>
{
    public async Task<Result<List<NameDto>>> Handle(GetPaymentTypeNameDtoListQuery request, CancellationToken cancellationToken)
    {
        var result = await paymentTypeRepository.GetPaymentTypeNameDtoList(request.Lang);
        return result;
    }
}
