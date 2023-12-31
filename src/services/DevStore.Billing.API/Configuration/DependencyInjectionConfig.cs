using DevStore.Billing.API.Data;
using DevStore.Billing.API.Data.Repository;
using DevStore.Billing.API.Facade;
using DevStore.Billing.API.Models;
using DevStore.Billing.API.Services;
using DevStore.WebAPI.Core.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DevStore.Billing.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<IBillingService, BillingService>();
        services.AddScoped<IPaymentFacade, CreditCardPaymentFacade>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<BillingContext>();
    }
}