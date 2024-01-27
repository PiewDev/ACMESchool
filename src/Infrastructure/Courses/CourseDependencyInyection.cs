using Domain.Courses;
using Infrastructure.Courses.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Courses;
public static class CourseDependecyInjection
{
    public static IServiceCollection AddCourseInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}