// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiController.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Hapi.Abstractions;
using Nomis.HapiExplorer.Interfaces;
using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Hapi
{
    /// <summary>
    /// A controller to aggregate all HAPI-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Hapi.")]
    internal sealed partial class HapiController :
        HapiBaseController
    {
        private readonly ILogger<HapiController> _logger;
        private readonly IHapiExplorerService _hapiExplorerService;

        /// <summary>
        /// Initialize <see cref="HapiController"/>.
        /// </summary>
        /// <param name="hapiExplorerService"><see cref="IHapiExplorerService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public HapiController(
            IHapiExplorerService hapiExplorerService,
            ILogger<HapiController> logger)
        {
            _hapiExplorerService = hapiExplorerService ?? throw new ArgumentNullException(nameof(hapiExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get risk score for given wallet address.
        /// </summary>
        /// <param name="network" example="ethereum">Blockchain network.</param>
        /// <param name="address" example="0xa0c7BD318D69424603CBf91e9969870F21B8ab4c">Hapi wallet address to get risk score.</param>
        /// <returns>An risk score value and corresponding data.</returns>
        /// <remarks>
        /// Sample request:
        ///     GET /api/v1/hapi/ethereum/wallet/0xa0c7BD318D69424603CBf91e9969870F21B8ab4c/score
        /// </remarks>
        /// <response code="200">Returns HAPI risk score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("{network}/wallet/{address}/score", Name = "GetHapiWalletRiskScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetHapiWalletRiskScore",
            Tags = new[] { HapiTag })]
        [ProducesResponseType(typeof(Result<HapiProxyRiskScoreResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetHapiWalletScoreAsync(
            [Required(ErrorMessage = "Network should be set")] string network,
            [Required(ErrorMessage = "Wallet address should be set")] string address)
        {
            var result = await _hapiExplorerService.GetWalletRiskScoreAsync(network, address);
            return Ok(result);
        }
    }
}