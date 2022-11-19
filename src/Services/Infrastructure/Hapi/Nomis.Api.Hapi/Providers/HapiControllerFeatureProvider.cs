// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiControllerFeatureProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Nomis.Api.Common.Providers;
using Nomis.Api.Hapi.Settings;

namespace Nomis.Api.Hapi.Providers
{
    /// <summary>
    /// A provider to check if the type is a controller.
    /// </summary>
    internal sealed class HapiControllerFeatureProvider :
        BaseControllerFeatureProvider
    {
        private readonly HapiAPISettings _hapiSettings;

        /// <summary>
        /// Initialize <see cref="HapiControllerFeatureProvider"/>.
        /// </summary>
        /// <param name="hapiSettings"><see cref="HapiAPISettings"/>.</param>
        public HapiControllerFeatureProvider(
            HapiAPISettings hapiSettings)
        {
            _hapiSettings = hapiSettings;
        }

        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            return base.IsController(typeInfo) && _hapiSettings.APIEnabled && typeInfo.Name == nameof(HapiController);
        }
    }
}