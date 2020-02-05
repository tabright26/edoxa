// Filename: PaypalService.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
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

        public async Task<PayoutBatch> WithdrawAsync(
            string transactionId,
            string email,
            int amount,
            string description = null,
            string correlationId = null
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

            return await Task.FromResult(Payout.Create(Options.GetApiContext(), payout));
        }
    }
}
