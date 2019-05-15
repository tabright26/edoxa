using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Commands.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/account")]
    public sealed class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("verify", Name = nameof(VerifyAccount))]
        public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }
    }
}
