using BoilerplateService.Infrastructures.Startup.PipelineExtensions;
using BoilerplateService.Infrastructures.Startup.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGeneralConfigurations(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddInjectedServices(builder.Configuration);
builder.Services.AddDynamoDbContext(builder.Configuration);

builder.Host
    .UseSerilog(BoilerplateService.Infrastructures.Loggings.LoggerFactory.SetupLogger);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseGeneralConfigurations(app.Environment);
app.UseSwaggerExposer(app.Environment);

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
