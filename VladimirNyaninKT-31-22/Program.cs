using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using VladimirNyaninKT_31_22.Database;

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

    //builder.Services.AddServices();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

    }

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




