using System.Text;
using ChildGrowth.API.Services.Implement;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Repository.Implement;
using ChildGrowth.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChildGrowth.API.Extensions;

public static class DependencyServices
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<ChildGrowDBContext>, UnitOfWork<ChildGrowDBContext>>();
        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        services.AddDbContext<ChildGrowDBContext>(options => options.UseSqlServer(CreateConnectionString(configuration)));
        return services;
    }

    private static string CreateConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        return connectionString;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IConsultationService, ConsultationService>();
        services.AddScoped<IGrowthRecordService, GrowthRecordService>();
        services.AddScoped<IChildService, ChildService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMembershipPlanService, MembershipPlanService>();
        return services;
    }

    public static IServiceCollection AddJwtValidation(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "ChildGrowthSystem",
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("PRN231SE1731095AESIEUNHAN12345678PRN231SE1731095AESIEUNHAN12345678PRN231SE1731095AESIEUNHAN12345678"))
            };
        });
        return services;
    }

    public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo() {Title = "Child Growth System", Version = "v1"});
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            options.MapType<TimeOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "time",
                Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
            });
        });
        return services;
    }
}