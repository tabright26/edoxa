// Filename: PaymentIntentController.cs
// Date Created: 2020-02-13
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Payment.Api.Controllers.Stripe
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/stripe/payment-intent")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PaymentIntentController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync()
        {
            var service = new PaymentIntentService();

            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
                Currency = "cad"
            };

            var paymentIntent = await service.CreateAsync(options);

            return this.Ok(paymentIntent.ClientSecret);
        }
    }
}
