using EmployeeService.Api;
using EmployeeService.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddAutoMapper(typeof(Program));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpCodeAndLogMiddleware();
    app.UseHsts();
}
else
{
    /*app.UseHttpCodeAndLogMiddleware();*/
}    

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
