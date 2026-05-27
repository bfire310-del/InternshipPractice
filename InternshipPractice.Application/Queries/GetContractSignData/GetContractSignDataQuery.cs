using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractSignData;

public record GetContractSignDataQuery(
    Guid UserId,
    string Lang, 
    Guid ContractId
):IRequest<Result<ContractSignDataResponse>>;
