// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiServiceRegistrar.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.HapiExplorer.Extensions;
using Nomis.HapiExplorer.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.HapiExplorer
{
    /// <inheritdoc cref="IServiceRegistrar"/>
    internal sealed class HapiServiceRegistrar :
        IHapiServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddHapiExplorerService();
        }
    }
}