using System.IO;
using DevStore.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevStore.WebApp.MVC.Configuration;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"/var/data_protection_keys/"))
            .SetApplicationName("DevStoreEnterprise");

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        services.Configure<AppSettings>(configuration);

        services.AddHealthChecks();
    }

    public static void UseMvcConfiguration(this WebApplication app)
    {
        app.UseForwardedHeaders();

        app.UseExceptionHandler("/error/500");
        app.UseStatusCodePagesWithRedirects("/error/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfiguration();

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapHealthChecks("/healthz");

        app.MapControllerRoute(
            "default",
            "{controller=Catalog}/{action=Index}/{id?}");
    }
}