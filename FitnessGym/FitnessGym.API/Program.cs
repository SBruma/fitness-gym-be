using FitnessGym.API.Configs;
using FitnessGym.API.Headers;
using FitnessGym.Application;
using FitnessGym.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ro") };
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.AddControllers().AddMvcLocalization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<LanguageHeaderParameterFilter>();
    option.UseDateOnlyTimeOnlyStringConverters();
});

builder.Services.ConfigureSwaggerOptions();
builder.Services.ConfigureAppOptions(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.Enrich.WithThreadId();
    configuration.Enrich.FromLogContext();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.ConfigureAuthenticationOptions(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseAuthentication();
app.UseAuthorization();

//app.UseIdentityServer();
app.MapControllers();

app.Run();
