// Filename: PaypalPayoutService.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Paypal.Services.Abstractions;

using Microsoft.Extensions.Options;

using PayPal.Api;

namespace eDoxa.Paypal.Services
{
    public sealed class PaypalPayoutService : IPaypalPayoutService
    {
        private readonly OAuthTokenCredential _credential;
        private readonly IOptions<PaypalOptions> _options;

        public PaypalPayoutService(OAuthTokenCredential credential, IOptionsSnapshot<PaypalOptions> options)
        {
            _credential = credential;
            _options = options;
        }

        private PaypalOptions Options => _options.Value;

        private APIContext Context => new APIContext(_credential.GetAccessToken());

        public async Task<PayoutBatch> CreateAsync(
            string transactionId,
            string email,
            int amount,
            string? description = null,
            string? correlationId = null
        )
        {
            var payout = new Payout
            {
                sender_batch_header = new PayoutSenderBatchHeader
                {
                    email_subject = Options.Payout.Email.Subject,
                    sender_batch_id = correlationId ?? Guid.NewGuid().ToString(),
                    recipient_type = PayoutRecipientType.EMAIL
                },
                items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        amount = new Currency
                        {
                            currency = Options.Payout.Currency,
                            value = amount.ToString()
                        },
                        receiver = email,
                        note = description ?? Options.Payout.Email.Note,
                        sender_item_id = transactionId
                    }
                }
            };

            return await Task.FromResult(Payout.Create(Context, payout));
        }
    }
}
