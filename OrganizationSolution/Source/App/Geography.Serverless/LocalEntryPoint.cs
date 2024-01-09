using Framework.Configuration.Models;
using Geography.DataAccess;
using Framework.Configuration;

namespace Geography.Serverless;

/// <summary>
/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
/// </summary>
public class LocalEntryPoint
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var countryProvider = services.GetRequiredService<CountryTableCreationProvider>();
            await countryProvider.Initialize("Country");

            var stateProvider = services.GetRequiredService<StateTableCreationProvider>();
            await stateProvider.Initialize("State");

        }
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly }, args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}