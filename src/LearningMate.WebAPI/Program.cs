using LearningMate.WebAPI.StartupExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Host.UseSerilog(
    (context, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services);
    }
);

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.DependencyInjectionServices();

builder.Configuration.AddJsonFile("WeatherApiConfig.json", optional: true, reloadOnChange: true);

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
    });
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { } //make the auto-generated Program accessible programmatically
