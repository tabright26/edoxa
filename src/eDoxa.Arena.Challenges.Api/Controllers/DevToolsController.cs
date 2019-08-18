// Filename: DevelopmentOnlyController.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [DevTools]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api")]
    [ApiExplorerSettings(GroupName = "DevTools")]
    public sealed class DevToolsController : ControllerBase
    {
        private readonly IDbContextSeeder _dbContextSeeder;

        public DevToolsController(IDbContextSeeder dbContextSeeder)
        {
            _dbContextSeeder = dbContextSeeder;
        }

        [HttpPost("database/reset")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> DatabaseResetAsync()
        {
            await _dbContextSeeder.CleanupAsync();

            await _dbContextSeeder.SeedAsync();

            return this.Ok("The database has been reset to the default data.");
        }

        [HttpPost("database/cleanup")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> DatabaseCleanupAsync()
        {
            await _dbContextSeeder.CleanupAsync();

            return this.Ok("The database has been cleaned up.");
        }
    }
}
