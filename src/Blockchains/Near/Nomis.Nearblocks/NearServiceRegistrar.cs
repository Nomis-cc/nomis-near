// ------------------------------------------------------------------------------------------------------
// <copyright file="NearServiceRegistrar.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.Nearblocks.Extensions;
using Nomis.Nearblocks.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Nearblocks
{
    /// <inheritdoc cref="IServiceRegistrar"/>
    internal sealed class NearServiceRegistrar :
        INearServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddNearblocksService();
        }
    }
}