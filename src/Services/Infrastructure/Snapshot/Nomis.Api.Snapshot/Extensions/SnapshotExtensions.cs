// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Api.Snapshot.Providers;
using Nomis.Api.Snapshot.Settings;
using Nomis.Api.Snapshot.Swagger.Filters;
using Nomis.Snapshot.Interfaces;
using Nomis.Utils.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Snapshot.Extensions
{
    /// <summary>
    /// Snapshot extension methods.
    /// </summary>
    public static class SnapshotExtensions
    {
        /// <summary>
        /// Add Snapshot API.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddSnapshotAPI(
            this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var serviceRegistrar = serviceProvider.GetRequiredService<ISnapshotServiceRegistrar>();

            services.AddSettings<SnapshotAPISettings>(configuration);
            var settings = configuration.GetSettings<SnapshotAPISettings>();
            if (settings.APIEnabled)
            {
                return serviceRegistrar
                    .RegisterService(services);
            }

            return services;
        }

        /// <summary>
        /// Add Snapshot controllers.
        /// </summary>
        /// <param name="manager"><see cref="ApplicationPartManager"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="ApplicationPartManager"/>.</returns>
        public static ApplicationPartManager AddSnapshot(
            this ApplicationPartManager manager,
            IConfiguration configuration)
        {
            manager.FeatureProviders
                .Add(new SnapshotControllerFeatureProvider(configuration.GetSettings<SnapshotAPISettings>()));

            return manager;
        }

        /// <summary>
        /// Add Snapshot swagger filters.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/>.</param>
        /// <returns>Returns <see cref="SwaggerGenOptions"/>.</returns>
        public static SwaggerGenOptions AddSnapshotFilters(
            this SwaggerGenOptions options)
        {
            options.DocumentFilter<SnapshotHideMethodsFilter>();

            return options;
        }
    }
}