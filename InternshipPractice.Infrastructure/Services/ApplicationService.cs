using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Interfaces.Services;
using KDS.Primitives.FluentResult;
using InternshipPractice.Infrastructure.Data;

namespace InternshipPractice.Infrastructure.Services;

public class ApplicationService(InternshipPracticeDbContext dbContext, IApplicationRepository applicationRepository, IContractRepository contractRepository): IApplicationService
{
    public async Task<Result> ApproveApplicationWithContract(Guid userId, Guid applicationId)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            var approveResult = await applicationRepository.ApproveApplicationWithoutSave(userId, applicationId);

            if (approveResult.IsFailed)
            {
                await transaction.RollbackAsync();
                return approveResult;
            }

            var contractResult = await contractRepository.GenerateContractWithoutSave(userId, applicationId);

            if (contractResult.IsFailed)
            {
                await transaction.RollbackAsync();
                return contractResult;
            }

            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return Result.Failure(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    $"Ошибка при одобрении заявки и создании договора: {ex.Message}"));
        }
    }
}
