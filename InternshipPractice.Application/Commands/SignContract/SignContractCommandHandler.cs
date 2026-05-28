using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.SignContract;

public class SignContractCommandHandler(IContractService contractService) : IRequestHandler<SignContractCommand, Result<SignContractResponse>>
{
    public async Task<Result<SignContractResponse>> Handle(SignContractCommand request, CancellationToken cancellationToken)
    {
        return await contractService.SignContract(
            request.UserId,
            request.ContractId,
            request.Signature,
            request.Lang,
            cancellationToken);
    }
}
