// ------------------------------------------------------------------------------------------------------
// <copyright file="NearHideMethodsFilter.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nomis.Api.Common.Swagger.Filters;
using Nomis.Api.Near.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Near.Swagger.Filters
{
    /// <summary>
    /// Filter for hiding API from path in swagger documentation for Near.
    /// </summary>
    internal sealed class NearHideMethodsFilter :
        BaseHideMethodsFilter
    {
        private readonly NearAPISettings _settings;

        /// <summary>
        /// Initialize <see cref="NearHideMethodsFilter"/>.
        /// </summary>
        /// <param name="settings"><see cref="NearAPISettings"/>.</param>
        public NearHideMethodsFilter(
            IOptions<NearAPISettings> settings)
        {
            _settings = settings.Value;
        }

        /// <inheritdoc/>
        public override void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!_settings.APIEnabled)
            {
                HideApiFromSwaggerPage(swaggerDoc, Abstractions.NearBaseController.NearTag);
            }
        }
    }
}