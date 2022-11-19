// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotServiceRegistrar.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.Snapshot.Extensions;
using Nomis.Snapshot.Interfaces;
using Nomis.Utils.Contracts.Services;

namespace Nomis.Snapshot
{
    /// <inheritdoc cref="IServiceRegistrar"/>
    internal sealed class SnapshotServiceRegistrar :
        ISnapshotServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddSnapshotService();
        }
    }
}