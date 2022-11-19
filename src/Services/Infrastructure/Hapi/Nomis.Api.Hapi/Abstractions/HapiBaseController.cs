// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiBaseController.cs" company="Nomis">
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

namespace Nomis.Api.Hapi.Abstractions
{
    /// <summary>
    /// Base controller for HAPI.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class HapiBaseController :
        BaseController
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/hapi";

        /// <summary>
        /// Common tag for HAPI actions.
        /// </summary>
        protected internal const string HapiTag = "Hapi";
    }

    /// <summary>
    /// A controller to aggregate all HAPI-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Hapi.")]
    internal sealed partial class HapiController :
        HapiBaseController
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
            Tags = new[] { HapiTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}