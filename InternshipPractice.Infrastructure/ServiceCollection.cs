using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Infrastructure.Data;
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
        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IWorkFormatRepository, WorkFormatRepository>();
        services.AddScoped<ITypeOfEmploymentRepository, TypeOfEmploymentRepository>();
        services.AddScoped<IPracticeFormRepository, PracticeFormRepository>();
        return services;
    }
}
