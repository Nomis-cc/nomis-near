// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseHideMethodsFilter.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nomis.Api.Common.Swagger.Filters
{
    /// <summary>
    /// Filter for hiding API from path in swagger documentation.
    /// </summary>
    public abstract class BaseHideMethodsFilter :
        IDocumentFilter
    {
        /// <inheritdoc/>
        public virtual void Apply(
            OpenApiDocument swaggerDoc,
            DocumentFilterContext context)
        {
        }

        /// <summary>
        /// Hide API from swagger page.
        /// </summary>
        /// <param name="swaggerDoc"><see cref="OpenApiDocument"/>.</param>
        /// <param name="name">Blockchain name.</param>
        protected static void HideApiFromSwaggerPage(
            OpenApiDocument swaggerDoc,
            string name)
        {
            foreach (var path in swaggerDoc.Paths.Where(x =>
                         x.Key.StartsWith($"/api/v1/{name}", StringComparison.InvariantCultureIgnoreCase)))
            {
                swaggerDoc.Paths.Remove(path.Key);
            }

            var tag = swaggerDoc.Tags.FirstOrDefault(x =>
                x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (tag != null)
            {
                swaggerDoc.Tags.Remove(tag);
            }
        }
    }
}