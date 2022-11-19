// ------------------------------------------------------------------------------------------------------
// <copyright file="ApiExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Nomis.Api.Common.Providers;

namespace Nomis.Api.Common.Extensions
{
    /// <summary>
    /// API extension methods.
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Add default routes redirection.
        /// </summary>
        /// <param name="manager"><see cref="ApplicationPartManager"/>.</param>
        /// <returns>Returns <see cref="ApplicationPartManager"/>.</returns>
        public static ApplicationPartManager AddDefaultRoutesRedirection(
            this ApplicationPartManager manager)
        {
            manager.FeatureProviders
                .Add(new BaseControllerFeatureProvider());

            return manager;
        }
    }
}