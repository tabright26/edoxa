// Filename: WithdrawalProcessedIntegrationEvent.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.IntegrationEvents;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    internal sealed class WithdrawalProcessedIntegrationEvent : IntegrationEvent
    {
        public WithdrawalProcessedIntegrationEvent(
            Guid transactionId,
            string transactionDescription,
            string connectAccountId,
            long amount
        )
        {
            TransactionId = transactionId;
            TransactionDescription = transactionDescription;
            ConnectAccountId = connectAccountId;
            Amount = amount;
        }

        public Guid TransactionId { get; private set; }

        public string TransactionDescription { get; private set; }

        public string ConnectAccountId { get; private set; }

        public long Amount { get; private set; }
    }
}
