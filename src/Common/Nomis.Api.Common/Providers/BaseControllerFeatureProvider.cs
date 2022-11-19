// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseControllerFeatureProvider.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Nomis.Api.Common.Providers
{
    /// <summary>
    /// A provider to check if the type is a controller.
    /// </summary>
    public class BaseControllerFeatureProvider :
        ControllerFeatureProvider
    {
        /// <summary>
        /// Controller name suffix.
        /// </summary>
        private const string ControllerSuffix = "Controller";

        /// <summary>
        /// Initialize <see cref="BaseControllerFeatureProvider"/>.
        /// </summary>
        public BaseControllerFeatureProvider()
        {
        }

        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            if (typeInfo.ContainsGenericParameters)
            {
                return false;
            }

            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }

            if (!typeInfo.Name.EndsWith(ControllerSuffix, StringComparison.OrdinalIgnoreCase) &&
                !typeInfo.IsDefined(typeof(ControllerAttribute)))
            {
                return false;
            }

            return true;
        }
    }
}