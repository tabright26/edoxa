// Filename: StripeTransferService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripeTransferService : TransferService, IStripeTransferService
    {
        public StripeTransferService(IOptions<StripeOptions> options)
        {
            Options = options.Value.Transfer;
        }

        private StripeTransferOptions Options { get; }

        public async Task<Transfer> CreateTransferAsync(
            string accountId,
            TransactionId transactionId,
            long amount,
            string description
        )
        {
            return await this.CreateAsync(
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
