using Domain.Payments;
using Infrastructure.Payments.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Payments;
public static class MakePaymentDependecyInjection
{
    public static IServiceCollection AddMakePaymentInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }
}

