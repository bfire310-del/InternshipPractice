using InternshipPractice.Application.Queries.GetVacancyCategoryNameDtoList;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipPractice.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetVacancyCategoryNameDtoListQuery).Assembly);
        });
        return services;
    }
}
