using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using VladimirNyaninKT_31_22.Database;
using VladimirNyaninKT_31_22.Middlewares;
using static VladimirNyaninKT_31_22.ServiceExtensions.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddDbContext<UniversityDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddServices();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

    }

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseAuthentication();
    app.MapControllers();
    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");


}
finally
{
    LogManager.Shutdown();

}




