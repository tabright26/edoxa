// Filename: BankAccountsController.cs
// Date Created: 2019-10-08
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
    [Route("api/stripe/bank-accounts")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class BankAccountsController : ControllerBase
    {
    }
}
