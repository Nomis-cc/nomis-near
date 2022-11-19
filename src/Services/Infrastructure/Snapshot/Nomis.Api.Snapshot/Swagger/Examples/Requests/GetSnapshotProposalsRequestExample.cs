// ------------------------------------------------------------------------------------------------------
// <copyright file="GetSnapshotProposalsRequestExample.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Nomis.Api.Snapshot.Swagger.Examples.Requests
{
    /// <summary>
    /// <see cref="GetSnapshotProposalsRequest"/> example.
    /// </summary>
    public class GetSnapshotProposalsRequestExample :
        IExamplesProvider<GetSnapshotProposalsRequest>
    {
        /// <inheritdoc/>
        public GetSnapshotProposalsRequest GetExamples()
        {
            return new()
            {
                OrderBy = "vp",
                OrderDirection = "desc",
                First = 300000,
                Skip = 0,
                Author = "0x9Ebc8AD4011C7f559743Eb25705CCF5A9B58D0bc",
                Spaces = new()
            };
        }
    }
}