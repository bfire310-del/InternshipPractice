using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.DownloadContract;

public record DownloadContractQuery(
    Guid UserId,
    string Lang,
    Guid ContractId
) : IRequest<Result<FileResponse>>;