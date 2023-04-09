using FitnessGym.API.Headers;
using FitnessGym.Application;
using FitnessGym.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ro") };

builder.Services.AddControllers().AddMvcLocalization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<LanguageHeaderParameterFilter>();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});


app.UseAuthorization();

app.MapControllers();

app.Run();
