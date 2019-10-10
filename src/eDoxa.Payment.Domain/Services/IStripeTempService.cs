// Filename: IStripeService.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeTempService
    {
        Task CreateInvoiceAsync(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount,
            CancellationToken cancellationToken = default
        );

        Task CreateTransferAsync(
            Guid transactionId,
            string transactionDescription,
            string connectAccountId,
            long amount,
            CancellationToken cancellationToken = default
        );
    }
}
