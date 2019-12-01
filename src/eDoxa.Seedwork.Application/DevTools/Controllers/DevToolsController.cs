// Filename: DevToolsController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application.DevTools.Attributes;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Seedwork.Application.DevTools.Controllers
{
    [Authorize]
    [DevTools]
    [ApiController]
    [ApiVersion("1.0")]
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

        [HttpPost("reset")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> ResetAsync()
        {
            await _cleaner.CleanupAsync();

            await _seeder.SeedAsync();

            return this.Ok("The database has been reset to the default data.");
        }

        [HttpPost("cleanup")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> CleanupAsync()
        {
            await _cleaner.CleanupAsync();

            return this.Ok("The database has been cleaned up.");
        }
    }
}
