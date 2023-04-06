using BankingManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Rate.Wroker;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Host Starting");

    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((configuration, services) =>
        {
            services.AddDbContext<BankingManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHostedService<RatesWorker>();
        })
        .Build();

    Log.Warning("Staring app");
    await host.RunAsync();
    Log.Information("Host Stopped");
}
catch (Exception ex)
{
    Log.Fatal(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}