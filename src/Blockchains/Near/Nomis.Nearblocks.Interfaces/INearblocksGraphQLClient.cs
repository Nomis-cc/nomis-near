// ------------------------------------------------------------------------------------------------------
// <copyright file="INearblocksGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.Nearblocks.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with Nearblocks API.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface INearblocksGraphQLClient :
        IGraphQLClient
    {
    }
}