// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Nearblocks.Interfaces;

using Nomis.Nearblocks.Settings;
using Nomis.ScoringService.Interfaces;
using Nomis.Utils.Extensions;

namespace Nomis.Nearblocks.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Near service registrar.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNearServiceRegistrar(
            this IServiceCollection services)
        {
            return services
                .AddSingleton<INearServiceRegistrar, NearServiceRegistrar>();
        }

        /// <summary>
        /// Add Nearblocks service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddNearblocksService(
            this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var settings = configuration.GetSettings<NearblocksSettings>();
            var scoringService = serviceProvider.GetRequiredService<IScoringService>();
            services.AddSingleton<INearblocksGraphQLClient>(_ =>
            {
                var graphQlOptions = new GraphQLHttpClientOptions
                {
                    EndPoint = new(settings.ApiBaseUrl!)
                };
                return new NearblocksGraphQLClient(graphQlOptions, new SystemTextJsonSerializer());
            });

            return services.AddSingleton<INearblocksService>(ctx =>
            {
                var graphQlClient = ctx.GetRequiredService<INearblocksGraphQLClient>();
                return new NearblocksService(settings, graphQlClient, scoringService);
            });
        }
    }
}