using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Entities;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Services;

public class ContractService(InternshipPracticeDbContext dbContext, IContractSignatureVerifier contractSignatureVerifier) : IContractService
{
    public async Task<Result<SignContractResponse>> SignContract(
        Guid userId,
        Guid contractId,
        string signature,
        string lang,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(signature))
        {
            return Result.Failure<SignContractResponse>(
                new Error(
                    Domain.Common.Error.BadRequest,
                    "Подпись не передана"));
        }

        var contract = await dbContext.Contracts
            .Include(c => c.Status)
            .Include(c => c.Application)
                .ThenInclude(a => a.Student)
            .Include(c => c.Application)
                .ThenInclude(a => a.Vacancy)
                .ThenInclude(a => a.Employer)
            .Include(c => c.Application)
                .ThenInclude(a => a.Student)
                .ThenInclude(s => s.Faculty)
                .ThenInclude(f => f.University)
                .ThenInclude(u => u.CareerCenters)
            .FirstOrDefaultAsync(c => c.ContractId == contractId, cancellationToken);

        if (contract is null)
        {
            return Result.Failure<SignContractResponse>(
                new Error(
                    Domain.Common.Error.NotFound,
                    "Договор не найден"));
        }

        var currentStatusCode = contract.Status.Code;

        var signerType = currentStatusCode switch
        {
            "waiting_student_sign" => "student",
            "waiting_employer_sign" => "employer",
            "waiting_university_sign" => "university",
            _ => null
        };

        if (signerType is null)
        {
            return Result.Failure<SignContractResponse>(
                new Error(
                    Domain.Common.Error.BadRequest,
                    "Договор сейчас нельзя подписать"));
        }

        var hasAccess = signerType switch
        {
            "student" => contract.Application.Student.UserId == userId,
            "employer" => contract.Application.Vacancy.Employer.UserId == userId,
            "university" => contract.Application.Student.Faculty.University.CareerCenters.Any(x => x.UserId == userId),
            _ => false
        };

        if (!hasAccess)
        {
            return Result.Failure<SignContractResponse>(
                new Error(
                    Domain.Common.Error.Forbidden,
                    "У вас нет прав для подписания этого договора"));
        }

        var dataToSign = lang switch
        {
            "kk" => contract.GeneratedContentKk,
            "en" => contract.GeneratedContentEn,
            _ => contract.GeneratedContentRu
        };

        if (string.IsNullOrWhiteSpace(dataToSign))
        {
            return Result.Failure<SignContractResponse>(
                new Error(
                    Domain.Common.Error.NotFound,
                    "Содержимое договора не найдено"));
        }

        var verifyResult = await contractSignatureVerifier.VerifyAsync(
            dataToSign,
            signature,
            cancellationToken);

        if (verifyResult.IsFailed)
            return Result.Failure<SignContractResponse>(verifyResult.Error);

        dbContext.ContractSignatures.Add(new ContractSignature
        {
            ContractSignatureId = Guid.NewGuid(),
            ContractId = contract.ContractId,
            SignerUserId = userId,
            SignerType = signerType,
            Lang = lang,
            Signature = signature,
            SignedData = dataToSign,
            SignedAt = DateTime.UtcNow
        });

        var nextStatusCode = currentStatusCode switch
        {
            "waiting_student_sign" => "waiting_employer_sign",
            "waiting_employer_sign" => "waiting_university_sign",
            "waiting_university_sign" => "fully_signed",
            _ => currentStatusCode
        };

        var nextStatusId = await dbContext.ContractStatuses
            .Where(s => s.Code == nextStatusCode && s.DeletedAt == null)
            .Select(s => s.ContractStatusId)
            .FirstOrDefaultAsync(cancellationToken);

        if (nextStatusId == Guid.Empty)
        {
            return Result.Failure<SignContractResponse>(
                new Error(
                    Domain.Common.Error.NotFound,
                    $"Статус договора {nextStatusCode} не найден"));
        }

        contract.StatusId = nextStatusId;
        contract.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new SignContractResponse
        {
            ContractId = contract.ContractId,
            StatusCode = nextStatusCode,
            Message = "Договор успешно подписан"
        });
    }
}
