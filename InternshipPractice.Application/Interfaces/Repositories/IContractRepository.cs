using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IContractRepository
{
    Task<Result> GenerateContractWithoutSave(Guid userId, Guid applicationId);
    Task<Result<PagedResult<ContractResponse>>> GetContractsByUserId(Guid userId, string lang, int page, int pageSize);
    Task<Result<ContractDetailResponse>> GetContractDetails(Guid userId, string lang, Guid contractId);
    Task<Result<ContractSignDataResponse>> GetContractSignData(Guid userId, string lang, Guid contractId);
    Task<Result<int>> GetActiveContractsCount(Guid userId);
    Task<Result<int>> GetWaitingForSignContractsCount(Guid userId);
    Task<Result<int>> GetCompletedContractsCount(Guid userId);
}
