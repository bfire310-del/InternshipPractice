using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.SignContract;

public record SignContractCommand(
    Guid UserId,
    Guid ContractId,
    string Signature,
    string Lang
    ):IRequest<Result<SignContractResponse>>;
