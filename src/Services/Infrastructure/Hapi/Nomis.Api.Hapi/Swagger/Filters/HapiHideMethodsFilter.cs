// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiHideMethodsFilter.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nomis.Api.Common.Swagger.Filters;
using Nomis.Api.Hapi.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Hapi.Swagger.Filters
{
    /// <summary>
    /// Filter for hiding API from path in swagger documentation for Hapi.
    /// </summary>
    internal sealed class HapiHideMethodsFilter :
        BaseHideMethodsFilter
    {
        private readonly HapiAPISettings _settings;

        /// <summary>
        /// Initialize <see cref="HapiHideMethodsFilter"/>.
        /// </summary>
        /// <param name="settings"><see cref="HapiAPISettings"/>.</param>
        public HapiHideMethodsFilter(
            IOptions<HapiAPISettings> settings)
        {
            _settings = settings.Value;
        }

        /// <inheritdoc/>
        public override void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!_settings.APIEnabled)
            {
                HideApiFromSwaggerPage(swaggerDoc, Abstractions.HapiBaseController.HapiTag);
            }
        }
    }
}