// Filename: AccountVerifyController.cs
// Date Created: 2019-10-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/stripe/account/verify")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class AccountVerifyController : ControllerBase
    {
    }
}
