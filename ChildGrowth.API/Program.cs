
using System.Text.Json.Serialization;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Extensions;
using ChildGrowth.API.Middleware;
using NLog.Web;

var logger = NLog.LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"))
    .GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: CorsConstant.PolicyName,
            policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
    });
    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    builder.Services.AddDatabase();
    builder.Services.AddUnitOfWork();
    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddJwtValidation();
    builder.Services.AddConfigSwagger();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseCors(CorsConstant.PolicyName);
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stop program because of exception");
}
finally
{
    NLog.LogManager.Shutdown();
}