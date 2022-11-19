// ------------------------------------------------------------------------------------------------------
// <copyright file="GetSnapshotVotesRequestExample.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Snapshot.Interfaces.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Nomis.Api.Snapshot.Swagger.Examples.Requests
{
    /// <summary>
    /// <see cref="GetSnapshotVotesRequest"/> example.
    /// </summary>
    public class GetSnapshotVotesRequestExample :
        IExamplesProvider<GetSnapshotVotesRequest>
    {
        /// <inheritdoc/>
        public GetSnapshotVotesRequest GetExamples()
        {
            return new()
            {
                OrderBy = "vp",
                OrderDirection = "desc",
                First = 300000,
                Skip = 0,
                Voter = "0x653d63E4F2D7112a19f5Eb993890a3F27b48aDa5",
                Spaces = new()
            };
        }
    }
}