using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractDetails;

public record GetContractDetailsQuery(
    Guid UserId,
    string Lang,
    Guid ContractId
) : IRequest<Result<ContractDetailResponse>>;