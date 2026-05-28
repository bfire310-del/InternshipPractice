using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.DownloadContract;

public class DownloadContractQueryHandler(IContractRepository contractRepository, IFileGeneratorService fileGeneratorService) : IRequestHandler<DownloadContractQuery, Result<FileResponse>>
{
    public async Task<Result<FileResponse>> Handle(DownloadContractQuery request, CancellationToken cancellationToken)
    {
        var contractResult = await contractRepository.GetContractDetails(
            request.UserId,
            request.Lang,
            request.ContractId);

        if (contractResult.IsFailed)
            return Result.Failure<FileResponse>(contractResult.Error);

        try
        {
            var file = fileGeneratorService.GenerateDocx(contractResult.Value);
            return Result.Success(file);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure<FileResponse>(
                new Error(Domain.Common.Error.NotFound, ex.Message));
        }
    }
}
