// Filename: StaticOptionsController.cs
// Date Created: 2020-01-13
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Options;
using eDoxa.Grpc.Protos.Identity.Options;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eDoxa.Cashier.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/static/options")]
    [ApiExplorerSettings(GroupName = "Static")]
    public sealed class StaticOptionsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] IOptionsSnapshot<CashierApiOptions> snapshot)
        {
            return await Task.FromResult(this.Ok(snapshot.Value.Static));
        }
    }
}
