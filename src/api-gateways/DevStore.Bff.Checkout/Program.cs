using DevStore.Bff.Checkout.Configuration;
using DevStore.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Builder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger());

#region Configure Services

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

builder.Services.AddMessageBusConfiguration(builder.Configuration);

builder.Services.ConfigureGrpcServices(builder.Configuration);


var app = builder.Build();

#endregion

#region Configure Pipeline

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);

app.Run();

#endregion