// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Hapi.Settings
{
    /// <summary>
    /// HAPI API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class HapiAPISettings :
        ISettings
    {
        /// <summary>
        /// Hapi explorer API is enabled.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool APIEnabled { get; set; }
    }
}