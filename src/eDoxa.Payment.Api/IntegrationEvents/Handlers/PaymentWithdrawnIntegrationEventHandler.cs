// Filename: PaymentWithdrawnIntegrationEventHandler.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.IntegrationEvents;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class PaymentWithdrawnIntegrationEventHandler : IIntegrationEventHandler<PaymentWithdrawnIntegrationEvent>
    {
        public Task Handle(PaymentWithdrawnIntegrationEvent integrationEvent)
        {
            throw new NotImplementedException();
        }
    }
}
