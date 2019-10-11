// Filename: StripeTransferService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Services;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeTransferService : TransferService, IStripeTransferService
    {
        public StripeTransferService(IOptions<StripeOptions> options)
        {
            Options = options.Value;
        }

        private StripeOptions Options { get; }

        public async Task CreateTransferAsync(
            TransactionId transactionId,
            string description,
            string accountId,
            long amount
        )
        {
            await this.CreateAsync(
                new TransferCreateOptions
                {
                    Destination = accountId,
                    Currency = Options.Currency,
                    Amount = amount,
                    Description = description,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(transactionId)] = transactionId.ToString()
                    }
                });
        }
    }
}
