using Framework.Configuration;
using Framework.Configuration.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Geography.ServerlessTests
{
    /// <summary>
    /// Defines the <see cref="TestSetup" />.
    /// </summary>
    public class TestSetup<T>
    {
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        public async Task<T> Main(string[] args)
        {
            T obj;

            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            obj = services.GetRequiredService<T>();
            return obj;  
        }

        /// <summary>
        /// The CreateHostBuilder.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns> 
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly }, args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
