using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractSignData;

public class GetContractSignDataQueryHandler(IContractRepository contractRepository) : IRequestHandler<GetContractSignDataQuery, Result<ContractSignDataResponse>>
{
    public async Task<Result<ContractSignDataResponse>> Handle(GetContractSignDataQuery request, CancellationToken cancellationToken)
    {
        var result = await contractRepository.GetContractSignData(
            request.UserId,
            request.Lang,
            request.ContractId);

        if (result.IsFailed)
            return Result.Failure<ContractSignDataResponse>(result.Error);

        return Result.Success(result.Value);
    }
}
