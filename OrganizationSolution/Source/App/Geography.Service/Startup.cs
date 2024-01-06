namespace Geography.Service
{
    using Framework.Business;
    using Framework.Business.ServiceProvider.Storage;
    using Framework.Configuration.Models;
    using Framework.Service;
    using Framework.Service.Extension;
    using Geography.Business;
    using Geography.Business.Country.Manager;
    using Geography.Business.Country.Models;
    using Geography.Business.GraphQL;
    using Geography.DataAccess;
    using GraphQL;
    using GraphQL.Types;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));            
            services.AddAutoMapper(typeof(CountryMappingProfile).Assembly);
            services.AddManagers(typeof(CountryManager).Assembly);
            services.ConfigureClientServices();
            services.AddTransient<CountryTableCreationProvider>();
            services.AddTransient<StateTableCreationProvider>();
            services.AddScoped(typeof(IStorageManager<AmazonS3ConfigurationOptions>), typeof(StorageManager));
            services.ConfigureGraphQLServices();
            services.ConfigureSwagger();
        }

        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.AddProblemDetailsSupport();

            //app.UseSwagger(new[]
            //       {
            //          new SwaggerConfigurationModel(ApiConstants.ApiVersion, ApiConstants.ApiName, true),
            //          new SwaggerConfigurationModel(ApiConstants.JobsApiVersion, ApiConstants.JobsApiName, false)
            //        });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
