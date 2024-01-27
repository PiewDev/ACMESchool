using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Web.API;
using Web.API.Extensions;
using Web.API.Middlewares;

namespace Application.Students.UnitTest.GetStudentWithCoursesInDataRange;

public static class ProgramTest
{
    public static WebApplication BuildApplication()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddPresentation()
                       .AddInfrastructure(builder.Configuration)
                       .AddApplication();

        var app = builder.Build();

        // Configura el HTTP request pipeline (similar a tu program.cs)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }

        app.UseExceptionHandler("/error");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<GloblalExceptionHandlingMiddleware>();

        app.MapControllers();

        return app;
    }
}