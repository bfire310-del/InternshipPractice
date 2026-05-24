using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class ContractRepository(InternshipPracticeDbContext dbContext): IContractRepository
{
    public async Task<Result> GenerateContractWithoutSave(Guid userId, Guid applicationId)
    {
        try
        {
            var application = await dbContext.Applications
                .Include(a => a.Student)
                    .ThenInclude(s => s.User)
                .Include(a => a.Student)
                    .ThenInclude(s => s.Faculty)
                        .ThenInclude(f => f.University)
                .Include(a => a.Vacancy)
                    .ThenInclude(v => v.Region)
                .Include(a => a.Vacancy)
                    .ThenInclude(v => v.Employer)
                        .ThenInclude(e => e.Company)
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId == applicationId &&
                    a.DeletedAt == null);

            if (application is null)
            {
                return Result.Failure(new Error(Domain.Common.Error.NotFound, "Заявка не найдена"));
            }

            if (application.Vacancy?.Employer?.UserId != userId)
            {
                return Result.Failure(new Error(Domain.Common.Error.Forbidden, "Вы не можете создать договор для чужой заявки"));
            }

            if (application.StatusId == null)
            {
                return Result.Failure(new Error(Domain.Common.Error.BadRequest, "У заявки отсутствует статус"));
            }

            var approvedStatus = await dbContext.ApplicationStatuses
                .Where(x => x.Code == "approved")
                .Select(x => x.ApplicationStatusId)
                .FirstOrDefaultAsync();

            if (application.StatusId != approvedStatus)
            {
                return Result.Failure(new Error(Domain.Common.Error.BadRequest, "Договор можно создать только для одобренной заявки"));
            }

            var alreadyExists = await dbContext.Contracts
                .AnyAsync(c =>
                    c.ApplicationId == applicationId &&
                    c.DeletedAt == null);

            if (alreadyExists)
            {
                return Result.Failure(new Error(Domain.Common.Error.BadRequest, "Договор уже существует"));
            }

            var template = await dbContext.ContractTemplates
                .Where(t =>
                    t.IsActive &&
                    t.DeletedAt == null)
                .FirstOrDefaultAsync();

            if (template is null)
            {
                return Result.Failure(new Error(Domain.Common.Error.NotFound, "Шаблон договора не найден"));
            }

            var contractStatusId = await dbContext.ContractStatuses
                .Where(x =>
                    x.Code == "waiting_student_sign" &&
                    x.DeletedAt == null)
                .Select(x => x.ContractStatusId)
                .FirstOrDefaultAsync();

            if (contractStatusId == Guid.Empty)
            {
                return Result.Failure(new Error(Domain.Common.Error.NotFound, "Статус договора waiting_student_sign не найден"));
            }

            var contractNumber = $"КЗ-{DateTime.UtcNow.Year}-{Random.Shared.Next(1000, 9999)}";

            var studentFullName =
                $"{application.Student.User.LastName} " +
                $"{application.Student.User.FirstName} " +
                $"{application.Student.User.Patronymic}";

            var cityRu = application.Vacancy.Region?.NameRu ?? "Алматы";
            var cityKk = application.Vacancy.Region?.NameKk ?? cityRu;
            var cityEn = application.Vacancy.Region?.NameEn ?? cityRu;

            var universityRu = application.Student.Faculty.University.NameRu ?? "";
            var universityKk = application.Student.Faculty.University.NameKk ?? universityRu;
            var universityEn = application.Student.Faculty.University.NameEn ?? universityRu;

            var companyRu = application.Vacancy.Employer.Company.CompanyNameRu ?? "";
            var companyKk = application.Vacancy.Employer.Company.CompanyNameKk ?? companyRu;
            var companyEn = application.Vacancy.Employer.Company.CompanyNameEn ?? companyRu;

            var vacancyTitle = application.Vacancy.JobTitle ?? "";

            var startDate = application.Vacancy.StartDate?.ToString("dd.MM.yyyy") ?? "";
            var endDate = application.Vacancy.EndDate?.ToString("dd.MM.yyyy") ?? "";
            var currentDate = DateTime.UtcNow.ToString("dd.MM.yyyy");

            var generatedContentRu = FillTemplate(
                template.ContentRu,
                contractNumber,
                cityRu,
                currentDate,
                universityRu,
                companyRu,
                studentFullName,
                vacancyTitle,
                startDate,
                endDate);

            var generatedContentKk = FillTemplate(
                template.ContentKk ?? template.ContentRu,
                contractNumber,
                cityKk,
                currentDate,
                universityKk,
                companyKk,
                studentFullName,
                vacancyTitle,
                startDate,
                endDate);

            var generatedContentEn = FillTemplate(
                template.ContentEn ?? template.ContentRu,
                contractNumber,
                cityEn,
                currentDate,
                universityEn,
                companyEn,
                studentFullName,
                vacancyTitle,
                startDate,
                endDate);
            
            var contract = new Domain.Entities.Contract
            {
                ContractId = Guid.NewGuid(),
                ContractNumber = contractNumber,
                ContractTemplateId = template.ContractTemplateId,
                ApplicationId = application.ApplicationId,
                StatusId = contractStatusId,

                GeneratedContentRu = generatedContentRu,
                GeneratedContentKk = generatedContentKk,
                GeneratedContentEn = generatedContentEn,

                StartDate = application.Vacancy.StartDate,
                EndDate = application.Vacancy.EndDate,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId
            };

            dbContext.Contracts.Add(contract);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Domain.Common.Error.InternalServerError, $"Ошибка при генерации договора: {ex.Message}"));
        }
    }
    
    private static string FillTemplate(
        string template,
        string contractNumber,
        string city,
        string currentDate,
        string universityName,
        string companyName,
        string studentFullName,
        string vacancyTitle,
        string startDate,
        string endDate)
    {
        return template
            .Replace("{{contract_number}}", contractNumber)
            .Replace("{{city}}", city)
            .Replace("{{current_date}}", currentDate)
            .Replace("{{university_name}}", universityName)
            .Replace("{{university_representative}}", "Ректор")
            .Replace("{{company_name}}", companyName)
            .Replace("{{company_representative}}", "Директор")
            .Replace("{{student_full_name}}", studentFullName)
            .Replace("{{vacancy_title}}", vacancyTitle)
            .Replace("{{start_date}}", startDate)
            .Replace("{{end_date}}", endDate)
            .Replace("{{university_address}}", "г. Алматы")
            .Replace("{{university_bin}}", "000000000000")
            .Replace("{{company_address}}", "г. Алматы")
            .Replace("{{company_bin}}", "000000000000")
            .Replace("{{student_address}}", "г. Алматы")
            .Replace("{{student_iin}}", "000000000000");
    }
}
