using DevStore.Orders.Infra.Context;
using DevStore.WebAPI.Core.Configuration;
using DevStore.WebAPI.Core.DatabaseFlavor;
using DevStore.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static DevStore.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;

namespace DevStore.Orders.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureProviderForContext<OrdersContext>(DetectDatabase(configuration));

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        services.AddGenericHealthCheck();
    }

    public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthConfiguration();

        app.UseGenericHealthCheck("/healthz");

        app.MapControllers();
    }
}