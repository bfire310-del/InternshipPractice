using InternshipPractice.Domain.Interfaces.Repositories;
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
        var connectionString = configuration["DefaultConnection:"];
        services.AddDbContext<InternshipPracticeDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IVacancyCategoryRepository, VacancyCategoryRepository>();
        return services;
    }
}
