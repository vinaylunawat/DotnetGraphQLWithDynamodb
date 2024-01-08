namespace Geography.Serverless.Extensions
{
    using Framework.Configuration.Models;
    using Geography.DataAccess;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Geography.Business.Country.Models;
    using Framework.Business;
    using Geography.Business.Country.Manager;
    using Geography.Business.State.Models;

    /// <summary>
    /// Defines the <see cref="ServicesConfiguration" />.
    /// </summary>
    public static class ServicesConfiguration
    {

        public const string AuthenticationScheme = "Bearer";

        /// <summary>
        /// The ConfigureClientServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services)
        {            
            return services
                .ConfigureAutoMapper()
                //.AddManagers(typeof(CountryManager).Assembly)
                .ConfigureDbServices();

        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(CountryMappingProfile).Assembly);
            //services.AddAutoMapper(typeof(StateMappingProfile).Assembly);
            return services;
        }

        /// <summary>
        /// The ConfigureSwagger.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            ////var swaggerAssemblies = new[] { typeof(Program).Assembly, typeof(CountryCreateModel).Assembly, typeof(Model).Assembly };
            //services.AddSwaggerWithComments(ApiConstants.ApiName, ApiConstants.ApiVersion, swaggerAssemblies);
            //services.AddSwaggerWithComments(ApiConstants.JobsApiName, ApiConstants.JobsApiVersion, swaggerAssemblies);
            return services;
        }

        public static IServiceCollection ConfigureAwsCongnitoSecurity(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();

            services.AddCognitoIdentity();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = applicationOptions.CognitoAuthorityURL;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false
                };
            });
            return services;
        }

        public static IServiceCollection ConfigureDataProvider(this IServiceCollection services)
        {
            services.AddTransient<CountryTableCreationProvider>();
            services.AddTransient<StateTableCreationProvider>();
            services.AddTransient<ProofOfIdentityTableCreationProvider>();
            return services;
        }
    }
}
