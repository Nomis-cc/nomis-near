// ------------------------------------------------------------------------------------------------------
// <copyright file="SnapshotBaseController.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomis.Api.Common.Abstractions;
using Nomis.Api.Common.Swagger.Examples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Nomis.Api.Snapshot.Abstractions
{
    /// <summary>
    /// Base controller for Snapshot.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class SnapshotBaseController :
        BaseController
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/snapshot";

        /// <summary>
        /// Common tag for Snapshot actions.
        /// </summary>
        protected internal const string SnapshotTag = "Snapshot";
    }

    /// <summary>
    /// A controller to aggregate all Snapshot-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Snapshot.")]
    internal sealed partial class SnapshotController :
        SnapshotBaseController
    {
        /// <summary>
        /// Ping API.
        /// </summary>
        /// <remarks>
        /// For health checks.
        /// </remarks>
        /// <returns>Return "Ok" string.</returns>
        /// <response code="200">Returns "Ok" string.</response>
        [HttpGet(nameof(Ping))]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = nameof(Ping),
            Tags = new[] { SnapshotTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}