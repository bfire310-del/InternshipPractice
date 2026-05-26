using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetContractsByUserId;

public record GetContractsByUserIdQuery(
    Guid UserId,
    string Lang,
    int Page,
    int PageSize
) : IRequest<Result<PagedResult<ContractResponse>>>;