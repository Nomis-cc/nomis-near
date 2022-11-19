// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotHideMethodsFilter.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nomis.Api.Common.Swagger.Filters;
using Nomis.Api.Snapshot.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Snapshot.Swagger.Filters
{
    /// <summary>
    /// Filter for hiding API from path in swagger documentation for Snapshot.
    /// </summary>
    internal sealed class SnapshotHideMethodsFilter :
        BaseHideMethodsFilter
    {
        private readonly SnapshotAPISettings _settings;

        /// <summary>
        /// Initialize <see cref="SnapshotHideMethodsFilter"/>.
        /// </summary>
        /// <param name="settings"><see cref="SnapshotAPISettings"/>.</param>
        public SnapshotHideMethodsFilter(
            IOptions<SnapshotAPISettings> settings)
        {
            _settings = settings.Value;
        }

        /// <inheritdoc/>
        public override void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!_settings.APIEnabled)
            {
                HideApiFromSwaggerPage(swaggerDoc, Abstractions.SnapshotBaseController.SnapshotTag);
            }
        }
    }
}