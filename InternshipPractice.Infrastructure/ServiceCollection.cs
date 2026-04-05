using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Infrastructure.Data;
using InternshipPractice.Infrastructure.Repositories;
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
        return services;
    }
}
