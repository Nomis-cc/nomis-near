// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Snapshot.Settings
{
    /// <summary>
    /// Snapshot API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class SnapshotAPISettings :
        ISettings
    {
        /// <summary>
        /// Snapshot API is enabled.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool APIEnabled { get; set; }
    }
}