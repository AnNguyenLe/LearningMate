using System.Net.Mime;
using System.Text;
using Asp.Versioning;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.ConfigurationOptions.Jwt;
using LearningMate.Core.ConfigurationOptions.Smtp;
using LearningMate.Domain.IdentityEntities;
using LearningMate.Infrastructure.DbContext;
using LearningMate.WebAPI.ExceptionHandlers;
using LearningMate.WebAPI.Filters.ActionFilterAttributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace LearningMate.WebAPI.StartupExtensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services
            .AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));

                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                options.Filters.Add(new ModelBindingFailureFormatFilter());
            })
            .ConfigureApiBehaviorOptions(options => { });

        services
            .AddApiVersioning(options =>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "APIs version 1",
                    Version = "1.0"
                }
            );

            options.SwaggerDoc(
                "v2",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "APIs version 2",
                    Version = "2.0"
                }
            );
        });

        //Client App's scheme://domain:port must match or * for all the origin
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder =>
            {
                corsBuilder
                    .WithOrigins(configuration.GetValue<string[]>("AllowedOrigins") ?? ["*"])
                    .WithHeaders(
                        HeaderNames.Authorization,
                        HeaderNames.Origin,
                        HeaderNames.Accept,
                        HeaderNames.ContentType
                    )
                    .WithMethods(
                        HttpMethods.Get,
                        HttpMethods.Post,
                        HttpMethods.Put,
                        HttpMethods.Delete
                    );

                // corsBuilder.AllowCredentials();
            });
        });

        services.AddHttpLogging(o =>
        {
            o.LoggingFields =
                Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders
                | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("Default"))
                .UseSnakeCaseNamingConvention();
        });

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        services
            .AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<AppUser, AppRole, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<AppRole, ApplicationDbContext, Guid>>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration.GetValue<string>("Jwt:SecretKey") ?? string.Empty
                        )
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));
        services.Configure<SmtpConfiguration>(configuration.GetSection("SmtpSettings"));
        services.Configure<MyAppServerConfiguration>(configuration.GetSection("MyAppServer"));

        return services;
    }
}
