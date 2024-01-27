using Domain.Enrollments;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Enrollments;
public static class EnrollmentDependencyInyection
{
    public static IServiceCollection AddEnrollmentApplication(this IServiceCollection services)
    {
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        return services;
    }
}