using InternshipPractice.Application.Interfaces.HttpClients;
using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Infrastructure.Data;
using InternshipPractice.Infrastructure.HttpClients;
using InternshipPractice.Infrastructure.Options;
using InternshipPractice.Infrastructure.Repositories;
using InternshipPractice.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipPractice.Infrastructure;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<InternshipPracticeDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IVacancyCategoryRepository, VacancyCategoryRepository>();
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IEmployerRepository, EmployerRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IEmployerApplicationRepository, EmployerApplicationRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        services.Configure<AccessOptions>(configuration.GetSection(AccessOptions.SectionName));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IWorkFormatRepository, WorkFormatRepository>();
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<ITypeOfEmploymentRepository, TypeOfEmploymentRepository>();
        services.AddScoped<IPracticeFormRepository, PracticeFormRepository>();
        services.AddScoped<IFileGeneratorService, FileGeneratorService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IContractSignatureVerifier, ContractSignatureVerifier>();
        services.AddScoped<ICompanyCategoryRepository, CompanyCategoryRepository>();
        services.AddHttpClient<IAccessHttpClient, AccessHttpClient>();
        return services;
    }
}
