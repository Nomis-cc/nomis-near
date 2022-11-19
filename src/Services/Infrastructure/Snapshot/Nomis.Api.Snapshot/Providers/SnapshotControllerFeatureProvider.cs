// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotControllerFeatureProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Nomis.Api.Common.Providers;
using Nomis.Api.Snapshot.Settings;

namespace Nomis.Api.Snapshot.Providers
{
    /// <summary>
    /// A provider to check if the type is a controller.
    /// </summary>
    internal sealed class SnapshotControllerFeatureProvider :
        BaseControllerFeatureProvider
    {
        private readonly SnapshotAPISettings _snapshotSettings;

        /// <summary>
        /// Initialize <see cref="SnapshotControllerFeatureProvider"/>.
        /// </summary>
        /// <param name="snapshotSettings"><see cref="SnapshotAPISettings"/>.</param>
        public SnapshotControllerFeatureProvider(
            SnapshotAPISettings snapshotSettings)
        {
            _snapshotSettings = snapshotSettings;
        }

        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            return base.IsController(typeInfo) && _snapshotSettings.APIEnabled && typeInfo.Name == nameof(SnapshotController);
        }
    }
}