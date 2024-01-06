using Framework.Configuration.Models;
using Geography.Business;
using Geography.Business.Country.Models;
using Geography.Business.GraphQL;
using Geography.Serverless.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Geography.DataAccess;
using Framework.Business.ServiceProvider.Storage;
using Geography.Business.Country.Manager;
using Framework.Business;
using Microsoft.IdentityModel.Logging;
namespace Geography.Serverless;
using Framework.Service.Extension;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddControllersWithViews().AddNewtonsoftJson();
        services.AddAutoMapper(typeof(Startup));
        services.AddAutoMapper(typeof(CountryMappingProfile).Assembly);
        services.AddManagers(typeof(CountryManager).Assembly);

        //services.Configure<ApplicationOptions>(Configuration.GetSection("Application"));
        services.AddTransient<CountryTableCreationProvider>();
        services.AddTransient<StateTableCreationProvider>();
        services.AddScoped(typeof(IStorageManager<AmazonS3ConfigurationOptions>), typeof(StorageManager));        
        services.ConfigureClientServices();
        services.ConfigureGraphQLServices();
        services.ConfigureAwsCongnitoSecurity();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.AddProblemDetailsSupport();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseGraphQL<AppSchema>();
        app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}