using Application.Data;
using Domain.Primitives;
using Infrastructure.Common.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Students;
using Infrastructure.Courses;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddStudentInfraestructure(configuration);
        services.AddCourseInfraestructure(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddScoped<IApplicationDbContext>(sp => 
                sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp => 
                sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}