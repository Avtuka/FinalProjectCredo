using Serilog;

namespace BankingManagementOnlineBanking.API.Infrastructure.Logger
{
    public static class SeriLogConfiguration
    {
        public static WebApplicationBuilder ConfigureSeriLog(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Services.AddLogging(lg =>
            {
                lg.AddSerilog(dispose: true);
            });

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();
            return builder;
        }
    }
}