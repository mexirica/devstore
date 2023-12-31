using DevStore.Catalog.API.Data;
using DevStore.Catalog.API.Data.Repository;
using DevStore.Catalog.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DevStore.Catalog.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<CatalogContext>();
    }
}