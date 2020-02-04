// Filename: PaypalService.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Paypal.Extensions;
using eDoxa.Paypal.Services.Abstractions;

using Microsoft.Extensions.Options;

using PayPal.Api;

namespace eDoxa.Paypal.Services
{
    public sealed class PaypalService : IPaypalService
    {
        private readonly IOptions<PaypalOptions> _options;

        public PaypalService(IOptionsSnapshot<PaypalOptions> options)
        {
            _options = options;
        }

        private PaypalOptions Options => _options.Value;

        public async Task CreatePayoutAsync(Payout payout)
        {
            await Task.FromResult(Payout.Create(Options.GetApiContext(), payout));
        }
    }
}
