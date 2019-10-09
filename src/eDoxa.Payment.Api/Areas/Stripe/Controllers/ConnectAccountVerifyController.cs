using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/stripe/connect-account/verify")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class ConnectAccountVerifyController : ControllerBase
    {
        private readonly AccountService _accountService;

        public ConnectAccountVerifyController(AccountService accountService)
        {
            _accountService = accountService;
        }

        //public async Task<IActionResult> GetAsync()
        //{
            

        //    return this.Ok();
        //}
    }
}
