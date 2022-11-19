// ------------------------------------------------------------------------------------------------------
// <copyright file="NearExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Api.Near.Providers;
using Nomis.Api.Near.Settings;
using Nomis.Api.Near.Swagger.Filters;
using Nomis.Nearblocks.Interfaces;
using Nomis.Utils.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Near.Extensions
{
    /// <summary>
    /// Near extension methods.
    /// </summary>
    public static class NearExtensions
    {
        /// <summary>
        /// Add Near API.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddNearAPI(
            this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var serviceRegistrar = serviceProvider.GetRequiredService<INearServiceRegistrar>();

            services.AddSettings<NearAPISettings>(configuration);
            var settings = configuration.GetSettings<NearAPISettings>();
            if (settings.APIEnabled)
            {
                return serviceRegistrar
                    .RegisterService(services);
            }

            return services;
        }

        /// <summary>
        /// Add Near controllers.
        /// </summary>
        /// <param name="manager"><see cref="ApplicationPartManager"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="ApplicationPartManager"/>.</returns>
        public static ApplicationPartManager AddNear(
            this ApplicationPartManager manager,
            IConfiguration configuration)
        {
            manager.FeatureProviders
                .Add(new NearControllerFeatureProvider(configuration.GetSettings<NearAPISettings>()));

            return manager;
        }

        /// <summary>
        /// Add Near swagger filters.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/>.</param>
        /// <returns>Returns <see cref="SwaggerGenOptions"/>.</returns>
        public static SwaggerGenOptions AddNearFilters(
            this SwaggerGenOptions options)
        {
            options.DocumentFilter<NearHideMethodsFilter>();

            return options;
        }
    }
}