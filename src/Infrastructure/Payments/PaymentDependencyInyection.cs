using Domain.Payments;
using Infrastructure.Payments.Persistence;
using Infrastructure.Payments.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Payments;
public static class PaymentDependecyInjection
{
    public static IServiceCollection AddPaymentInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentGateway, PaymentGateway>();
        return services;
    }
}

