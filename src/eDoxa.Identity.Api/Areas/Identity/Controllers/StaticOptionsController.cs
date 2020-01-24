﻿// Filename: StaticOptionsController.cs
// Date Created: 2020-01-13
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Options;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/static/options")]
    [ApiExplorerSettings(GroupName = "Static")]
    public sealed class StaticOptionsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] IOptionsSnapshot<IdentityApiOptions> snapshot)
        {
            var options = snapshot.Value;

            foreach (var country in options.Static.Countries)
            {
                country.Address = options.TryOverridesAddressOptionsFor(country.IsoCode);
            }

            return await Task.FromResult(this.Ok(options.Static));
        }
    }
}