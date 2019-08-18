// Filename: DevToolsController.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Controllers
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
        public async Task<IActionResult> DatabaseResetAsync()
        {
            await _dbContextSeeder.CleanupAsync();

            await _dbContextSeeder.SeedAsync();

            return this.Ok("The database has been reset to the default data.");
        }

        [HttpPost("database/cleanup")]
        public async Task<IActionResult> DatabaseCleanupAsync()
        {
            await _dbContextSeeder.CleanupAsync();

            return this.Ok("The database has been cleaned up.");
        }
    }
}
