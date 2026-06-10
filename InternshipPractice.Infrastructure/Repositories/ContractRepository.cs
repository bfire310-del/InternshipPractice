using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
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

            var contractNumber = await GenerateContractNumberAsync();
            
            var studentFullName = string.Join(" ",
                new[]
                    {
                        application.Student.User.LastName,
                        application.Student.User.FirstName,
                        application.Student.User.Patronymic
                    }
                    .Where(x => !string.IsNullOrWhiteSpace(x)));

            var companyRegionRu = application.Vacancy.Region?.NameRu ?? "Алматы";
            var companyRegionKk = application.Vacancy.Region?.NameKk ?? companyRegionRu;
            var companyRegionEn = application.Vacancy.Region?.NameEn ?? companyRegionRu;

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
                companyRegionRu,
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
                companyRegionKk,
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
                companyRegionEn,
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

    public async Task<Result<PagedResult<ContractResponse>>> GetContractsByUserId(Guid userId, string lang, int page, int pageSize)
    {
        try
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 5 : pageSize;
            
            var studentId = await dbContext.Students
                .Where(s => s.UserId == userId && s.DeletedAt == null)
                .Select(s => s.StudentId)
                .FirstOrDefaultAsync();

            if (studentId == Guid.Empty)
            {
                return Result.Failure<PagedResult<ContractResponse>>(
                    new Error(Domain.Common.Error.NotFound,
                        "Студент для текущего пользователя не найден"));
            }
            
            var allowedApplicationStatuses = new[]
            {
                "approved",
                "contract_signed"
            };

            var query = dbContext.Contracts
                .AsNoTracking()
                .Where(c =>
                    c.Application.StudentId == studentId &&
                    c.Application.DeletedAt == null &&
                    allowedApplicationStatuses.Contains(c.Application.ApplicationStatus.Code));

            var totalCount = await query.CountAsync();

            var contracts = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ContractResponse
                {
                    ContractId = c.ContractId,

                    Status = lang == "kk"
                        ? c.Status.NameKk
                        : lang == "en"
                            ? c.Status.NameEn
                            : c.Status.NameRu,
                    JobTitle = c.Application.Vacancy.JobTitle,

                    CompanyName = lang == "kk"
                        ? c.Application.Vacancy.Employer!.Company!.CompanyNameKk
                        : lang == "en"
                            ? c.Application.Vacancy.Employer!.Company!.CompanyNameEn
                            : c.Application.Vacancy.Employer!.Company!.CompanyNameRu,

                    StartDate = c.Application.Vacancy.StartDate,
                    EndDate = c.Application.Vacancy.EndDate
                })
                .ToListAsync();

            return Result.Success(new PagedResult<ContractResponse>
            {
                Items = contracts,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            });
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<ContractResponse>>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении договоров студента: {ex.Message}"));
        }
    }

    public async Task<Result<PagedResult<ContractResponse>>> GetContractsByStuff(Guid userId, string lang, int page, int pageSize)
    {
        
        try
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 5 : pageSize;
            
            
            var allowedApplicationStatuses = new[]
            {
                "approved",
                "contract_signed"
            };

            var query = dbContext.Contracts
                .AsNoTracking()
                .Where(c =>
                    c.Application.DeletedAt == null &&
                    allowedApplicationStatuses.Contains(c.Application.ApplicationStatus.Code));


            var totalCount = await query.CountAsync();

            var contracts = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ContractResponse
                {
                    ContractId = c.ContractId,

                    Status = lang == "kk"
                        ? c.Status.NameKk
                        : lang == "en"
                            ? c.Status.NameEn
                            : c.Status.NameRu,
                    JobTitle = c.Application.Vacancy.JobTitle,
                    Student = c.Application.Student.User.LastName + " " + c.Application.Student.User.FirstName,

                    CompanyName = lang == "kk"
                        ? c.Application.Vacancy.Employer!.Company!.CompanyNameKk
                        : lang == "en"
                            ? c.Application.Vacancy.Employer!.Company!.CompanyNameEn
                            : c.Application.Vacancy.Employer!.Company!.CompanyNameRu,

                    StartDate = c.Application.Vacancy.StartDate,
                    EndDate = c.Application.Vacancy.EndDate
                })
                .ToListAsync();

            return Result.Success(new PagedResult<ContractResponse>
            {
                Items = contracts,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            });
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<ContractResponse>>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении договоров студента: {ex.Message}"));
        }
    }

    public async Task<Result<ContractDetailResponse>> GetContractDetails(Guid userId, string lang, Guid contractId)
    {
        try
        {
            var studentId = await dbContext.Students
                .Where(s => s.UserId == userId && s.DeletedAt == null)
                .Select(s => s.StudentId)
                .FirstOrDefaultAsync();

            //if (studentId == Guid.Empty)
            //{
                //return Result.Failure<ContractDetailResponse>(
              //      new Error(Domain.Common.Error.NotFound,
             //           "Студент для текущего пользователя не найден"));
            //}
            
            var contract = await dbContext.Contracts
                .AsNoTracking()
                .Where(c =>
                    c.ContractId == contractId)
                .Select(c => new ContractDetailResponse
                {
                    ContractId = c.ContractId,
                    ContractNumber = c.ContractNumber,
                    JobTitle = c.Application.Vacancy.JobTitle,
                    StartDate = c.Application.Vacancy.StartDate,
                    EndDate = c.Application.Vacancy.EndDate,

                    Student =
                        c.Application.Student.User.LastName + " " +
                        c.Application.Student.User.FirstName + " " +
                        (c.Application.Student.User.Patronymic ?? ""),

                    Company =
                        c.Application.Vacancy.Employer.User.LastName + " " +
                        c.Application.Vacancy.Employer.User.FirstName + " " +
                        (c.Application.Vacancy.Employer.User.Patronymic ?? ""),

                    University = lang == "kk"
                        ? c.Application.Student.Faculty.University.NameKk
                        : lang == "en"
                            ? c.Application.Student.Faculty.University.NameEn
                            : c.Application.Student.Faculty.University.NameRu,

                    IsStudentSigned =
                        c.Status.Code != "waiting_student_sign" &&
                        c.Status.Code != "cancelled",

                    IsEmployerSigned =
                        c.Status.Code == "waiting_university_sign" ||
                        c.Status.Code == "fully_signed" ||
                        c.Status.Code == "completed",

                    IsUniversitySigned =
                        c.Status.Code == "fully_signed" ||
                        c.Status.Code == "completed",

                    ContractContent = lang == "kk"
                        ? c.GeneratedContentKk
                        : lang == "en"
                            ? c.GeneratedContentEn
                            : c.GeneratedContentRu
                })
                .FirstOrDefaultAsync();

            if (contract is null)
            {
                return Result.Failure<ContractDetailResponse>(
                    new Error(Domain.Common.Error.NotFound, "Договор не найден"));
            }

            return Result.Success(contract);
        }
        catch (Exception ex)
        {
            return Result.Failure<ContractDetailResponse>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении договора студента: {ex.Message}"));
        }
    }

    public async Task<Result<ContractSignDataResponse>> GetContractSignData(Guid userId, string lang, Guid contractId)
    {
         try
        {
            var contract = await dbContext.Contracts
                .AsNoTracking()
                .Where(c =>
                    c.ContractId == contractId)
                .Select(c => new
                {
                    c.ContractId,
                    c.ContractNumber,
                    c.GeneratedContentRu,
                    c.GeneratedContentKk,
                    c.GeneratedContentEn
                })
                .FirstOrDefaultAsync();

            if (contract is null)
            {
                return Result.Failure<ContractSignDataResponse>(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Договор не найден"));
            }

            var dataToSign = lang switch
            {
                "kk" => contract.GeneratedContentKk,
                "en" => contract.GeneratedContentEn,
                _ => contract.GeneratedContentRu
            };

            if (string.IsNullOrWhiteSpace(dataToSign))
            {
                return Result.Failure<ContractSignDataResponse>(
                    new Error(
                        Domain.Common.Error.NotFound,
                        "Содержимое договора для подписания не найдено"));
            }

            return Result.Success(new ContractSignDataResponse
            {
                ContractId = contract.ContractId,
                ContractNumber = contract.ContractNumber,
                DataToSign = dataToSign
            });
        }
        catch (Exception ex)
        {
            return Result.Failure<ContractSignDataResponse>(
                new Error(
                    Domain.Common.Error.InternalServerError,
                    $"Ошибка при получении данных договора для подписания: {ex.Message}"));
        }
    }

    public async Task<Result<int>> GetActiveContractsCount(Guid userId)
    {
        try
        {
            var contractStatusId = await dbContext.ContractStatuses
                .Where(x =>
                    x.Code == "fully_signed" &&
                    x.DeletedAt == null)
                .Select(x => x.ContractStatusId)
                .FirstOrDefaultAsync();

            if (contractStatusId == Guid.Empty)
            {
                return Result.Failure<int>(new Error(Domain.Common.Error.NotFound, "Статус договора fully_signed не найден"));
            }
            
            var allowedApplicationStatuses = new[]
            {
                "approved",
                "contract_signed"
            };
            
            var count = await dbContext.Contracts
                .Include(c => c.Application)
                .ThenInclude(a => a.Student)
                .Where(x => x.StatusId == contractStatusId && x.Application.Student.UserId == userId &&
                            x.Application.DeletedAt == null &&
                            allowedApplicationStatuses.Contains(x.Application.ApplicationStatus.Code))
                .CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества активных договоров студента: {ex.Message}"));
        }
    }

    public async Task<Result<int>> GetWaitingForSignContractsCount(Guid userId)
    {
        try
        {
            var contractStatusWaitingStudentSignId = await dbContext.ContractStatuses
                .Where(x =>
                    x.Code == "waiting_student_sign" &&
                    x.DeletedAt == null)
                .Select(x => x.ContractStatusId)
                .FirstOrDefaultAsync();

            if (contractStatusWaitingStudentSignId == Guid.Empty)
            {
                return Result.Failure<int>(new Error(Domain.Common.Error.NotFound, "Статус договора waiting_student_sign не найден"));
            }
            
            var contractStatusWaitingEmployerSignId = await dbContext.ContractStatuses
                .Where(x =>
                    x.Code == "waiting_employer_sign" &&
                    x.DeletedAt == null)
                .Select(x => x.ContractStatusId)
                .FirstOrDefaultAsync();

            if (contractStatusWaitingEmployerSignId == Guid.Empty)
            {
                return Result.Failure<int>(new Error(Domain.Common.Error.NotFound, "Статус договора waiting_employer_sign не найден"));
            }
            
            var contractStatusWaitingUniversitySignId = await dbContext.ContractStatuses
                .Where(x =>
                    x.Code == "waiting_university_sign" &&
                    x.DeletedAt == null)
                .Select(x => x.ContractStatusId)
                .FirstOrDefaultAsync();

            if (contractStatusWaitingUniversitySignId == Guid.Empty)
            {
                return Result.Failure<int>(new Error(Domain.Common.Error.NotFound, "Статус договора waiting_university_sign не найден"));
            }
            
            var allowedApplicationStatuses = new[]
            {
                "approved",
                "contract_signed"
            };
            
            var count = await dbContext.Contracts
                .Include(c => c.Application)
                .ThenInclude(a => a.Student)
                .Where(x => (x.StatusId == contractStatusWaitingStudentSignId || 
                             x.StatusId == contractStatusWaitingEmployerSignId || x.StatusId == contractStatusWaitingUniversitySignId) && x.Application.Student.UserId == userId  &&
                            x.Application.DeletedAt == null &&
                            allowedApplicationStatuses.Contains(x.Application.ApplicationStatus.Code))
                .CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества договоров на подписании студента: {ex.Message}"));
        }
    }

    public async Task<Result<int>> GetCompletedContractsCount(Guid userId)
    {
        try
        {
            var contractStatusId = await dbContext.ContractStatuses
                .Where(x =>
                    x.Code == "completed" &&
                    x.DeletedAt == null)
                .Select(x => x.ContractStatusId)
                .FirstOrDefaultAsync();

            if (contractStatusId == Guid.Empty)
            {
                return Result.Failure<int>(new Error(Domain.Common.Error.NotFound, "Статус договора completed не найден"));
            }
            
            var allowedApplicationStatuses = new[]
            {
                "approved",
                "contract_signed"
            };
            
            var count = await dbContext.Contracts
                .Include(c => c.Application)
                .ThenInclude(a => a.Student)
                .Where(x => x.StatusId == contractStatusId && x.Application.Student.UserId == userId  &&
                            x.Application.DeletedAt == null &&
                            allowedApplicationStatuses.Contains(x.Application.ApplicationStatus.Code))
                .CountAsync();
            return count;
        }
        catch (Exception ex)
        {
            return Result.Failure<int>(new Error(Domain.Common.Error.InternalServerError,$"Ошибка при получении количества завершенных договоров студента: {ex.Message}"));
        }
    }

    private static string FillTemplate(
        string template,
        string contractNumber,
        string companyRegion,
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
            .Replace("{{city}}", companyRegion)
            .Replace("{{current_date}}", currentDate)
            .Replace("{{university_name}}", universityName)
            .Replace("{{university_representative}}", "Ректор")
            .Replace("{{company_name}}", companyName)
            .Replace("{{company_representative}}", "Директор")
            .Replace("{{student_full_name}}", studentFullName)
            .Replace("{{vacancy_title}}", vacancyTitle)
            .Replace("{{start_date}}", startDate)
            .Replace("{{end_date}}", endDate)
            .Replace("{{university_address}}", companyRegion)
            .Replace("{{company_address}}", companyRegion)
            .Replace("{{student_address}}", companyRegion);
    }
    
    private async Task<string> GenerateContractNumberAsync()
    {
        var nextValue = await dbContext.Database
            .SqlQueryRaw<long>("SELECT nextval('contract_number_seq') AS \"Value\"")
            .SingleAsync();

        return $"КЗ-{DateTime.UtcNow.Year}-{nextValue:D6}";
    }
}
