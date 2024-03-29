﻿using Domain.Students;
using Infrastructure.Students.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Students;
public static class StudentDependecyInjection
{
    public static IServiceCollection AddStudentInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
    }
}
