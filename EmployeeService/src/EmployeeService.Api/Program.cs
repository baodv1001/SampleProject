using AutoMapper;
using EmployeeService.Api;
using EmployeeService.Api.Helper;
using EmployeeService.Api.Middlewares;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
    builder.Host
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    })
    .UseNLog();

// Add services to the container.

// Configure Cors
builder.Services.ConfigureCors();

// Configure Dependency Injection
builder.Services.ConfigureDependencyInjection(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

//Configure Auto Mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");
IConfigurationBuilder configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, reloadOnChange: true);

if (currentEnvironment?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true)
{
    configBuilder.AddJsonFile($"appsettings.{currentEnvironment}.json", optional: false);
}

IConfigurationRoot config = configBuilder.Build();
LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
Logger logger = LogManager.GetCurrentClassLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpCodeAndLogMiddleware();
    app.UseHsts();
}
else
{
    app.UseHttpCodeAndLogMiddleware();
}

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    logger.Info($"{ApiConstants.FriendlyServiceName} starts running ...");
    app.Run();
    logger.Info($"{ApiConstants.FriendlyServiceName} is stopped ...");
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    LogManager.Shutdown();
}
