﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

namespace Nomis.Api.Common.Abstractions
{
    /// <summary>
    /// Base controller.
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route(BasePath + "/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        protected internal const string BasePath = "api/v{version:apiVersion}";

        /// <summary>
        /// API version from HTTP request.
        /// </summary>
        protected string ApiVersion => (HttpContext.GetRequestedApiVersion() ?? new ApiVersion(1, 0)).ToString();
    }
}