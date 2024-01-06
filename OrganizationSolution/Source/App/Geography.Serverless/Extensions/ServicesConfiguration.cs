﻿namespace Geography.Serverless.Extensions
{
    using Framework.Configuration.Models;    
    using Geography.DataAccess;    
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="ServicesConfiguration" />.
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// The ConfigureClientServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
            return services
                .ConfigureDbServices();
                
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
    }
}
