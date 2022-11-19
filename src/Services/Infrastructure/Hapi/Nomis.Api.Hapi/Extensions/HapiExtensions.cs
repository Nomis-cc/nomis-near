// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Api.Hapi.Providers;
using Nomis.Api.Hapi.Settings;
using Nomis.Api.Hapi.Swagger.Filters;
using Nomis.HapiExplorer.Interfaces;
using Nomis.Utils.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Hapi.Extensions
{
    /// <summary>
    /// Hapi extension methods.
    /// </summary>
    public static class HapiExtensions
    {
        /// <summary>
        /// Add HAPI API.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddHapiAPI(
            this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var serviceRegistrar = serviceProvider.GetRequiredService<IHapiServiceRegistrar>();

            services.AddSettings<HapiAPISettings>(configuration);
            var settings = configuration.GetSettings<HapiAPISettings>();
            if (settings.APIEnabled)
            {
                return serviceRegistrar
                    .RegisterService(services);
            }

            return services;
        }

        /// <summary>
        /// Add Hapi controllers.
        /// </summary>
        /// <param name="manager"><see cref="ApplicationPartManager"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="ApplicationPartManager"/>.</returns>
        public static ApplicationPartManager AddHapi(
            this ApplicationPartManager manager,
            IConfiguration configuration)
        {
            manager.FeatureProviders
                .Add(new HapiControllerFeatureProvider(configuration.GetSettings<HapiAPISettings>()));

            return manager;
        }

        /// <summary>
        /// Add HAPI swagger filters.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/>.</param>
        /// <returns>Returns <see cref="SwaggerGenOptions"/>.</returns>
        public static SwaggerGenOptions AddHapiFilters(
            this SwaggerGenOptions options)
        {
            options.DocumentFilter<HapiHideMethodsFilter>();

            return options;
        }
    }
}