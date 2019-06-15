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

using eDoxa.Seedwork.Application.Mvc.Filters.Attributes;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [DevelopmentOnly]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api")]
    [ApiExplorerSettings(GroupName = "DevelopmentOnly")]
    public sealed class DevelopmentOnlyController : ControllerBase
    {
        private readonly IDbContextData _dbContextData;

        public DevelopmentOnlyController(IDbContextData dbContextData)
        {
            _dbContextData = dbContextData;
        }

        [HttpPost("database/reset")]
        public async Task<IActionResult> DatabaseResetAsync()
        {
            await _dbContextData.CleanupAsync();

            await _dbContextData.SeedAsync();

            return this.Ok("The database has been reset to the default data.");
        }

        [HttpPost("database/cleanup")]
        public async Task<IActionResult> DatabaseCleanupAsync()
        {
            await _dbContextData.CleanupAsync();

            return this.Ok("The database has been cleaned up.");
        }
    }
}
