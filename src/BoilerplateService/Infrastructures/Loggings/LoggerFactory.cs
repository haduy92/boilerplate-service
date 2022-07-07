namespace BoilerplateService.Infrastructures.Loggings
{
    public class LoggerFactory
    {
        public static Serilog.ILogger CreateLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static void SetupLogger(HostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration)
        {
            var configuration = hostingContext.Configuration.GetSection("Serilog");

            if (bool.TrueString.Equals(configuration["RenderJson"], StringComparison.OrdinalIgnoreCase))
            {
                loggerConfiguration.WriteTo.Console(new RenderedCompactJsonFormatter());
            }
            else
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: configuration["ConsoleTemplate"],
                    restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(configuration["MinimumLevel"]));
            }
            loggerConfiguration.Enrich.FromLogContext();
        }
    }
}