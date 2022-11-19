// ------------------------------------------------------------------------------------------------------
// <copyright file="NearControllerFeatureProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Nomis.Api.Common.Providers;
using Nomis.Api.Near.Settings;

namespace Nomis.Api.Near.Providers
{
    /// <summary>
    /// A provider to check if the type is a controller.
    /// </summary>
    internal sealed class NearControllerFeatureProvider :
        BaseControllerFeatureProvider
    {
        private readonly NearAPISettings _nearSettings;

        /// <summary>
        /// Initialize <see cref="NearControllerFeatureProvider"/>.
        /// </summary>
        /// <param name="nearSettings"><see cref="NearAPISettings"/>.</param>
        public NearControllerFeatureProvider(
            NearAPISettings nearSettings)
        {
            _nearSettings = nearSettings;
        }

        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            return base.IsController(typeInfo) && _nearSettings.APIEnabled && typeInfo.Name == nameof(NearController);
        }
    }
}