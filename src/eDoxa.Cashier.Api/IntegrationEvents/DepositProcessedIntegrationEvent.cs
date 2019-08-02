// Filename: DepositProcessedIntegrationEvent.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    internal sealed class DepositProcessedIntegrationEvent : IntegrationEvent
    {
        public DepositProcessedIntegrationEvent(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount
        ) : base(Guid.NewGuid())
        {
            TransactionId = transactionId;
            TransactionDescription = transactionDescription;
            CustomerId = customerId;
            Amount = amount;
        }

        public Guid TransactionId { get; private set; }

        public string TransactionDescription { get; private set; }

        public string CustomerId { get; private set; }

        public long Amount { get; private set; }
    }
}
