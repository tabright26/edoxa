// Filename: DevToolsController.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        private readonly IDbContextSeeder _seeder;
        private readonly IDbContextCleaner _cleaner;

        public DevToolsController(IDbContextSeeder seeder, IDbContextCleaner cleaner)
        {
            _seeder = seeder;
            _cleaner = cleaner;
        }

        [HttpPost("database/reset")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> DatabaseResetAsync()
        {
            await _cleaner.CleanupAsync();

            await _seeder.SeedAsync();

            return this.Ok("The database has been reset to the default data.");
        }

        [HttpPost("database/cleanup")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> DatabaseCleanupAsync()
        {
            await _cleaner.CleanupAsync();

            return this.Ok("The database has been cleaned up.");
        }
    }
}
