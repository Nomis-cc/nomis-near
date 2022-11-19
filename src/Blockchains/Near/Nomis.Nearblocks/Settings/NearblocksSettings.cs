// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Nearblocks.Settings
{
    /// <summary>
    /// Nearblocks settings.
    /// </summary>
    internal class NearblocksSettings :
        ISettings
    {
        /// <summary>
        /// Nearblocks GraphQL API base address.
        /// </summary>
        public string? ApiBaseUrl { get; set; }

        /// <summary>
        /// Nearblocks Rest API base address.
        /// </summary>
        public string? RestApiBaseUrl { get; set; }
    }
}