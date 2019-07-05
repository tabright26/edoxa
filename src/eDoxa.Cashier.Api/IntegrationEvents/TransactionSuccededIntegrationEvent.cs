﻿// Filename: TransactionSuccededIntegrationEvent.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IntegrationEvents;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    public class TransactionSuccededIntegrationEvent : IntegrationEvent
    {
        public TransactionSuccededIntegrationEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }
        
        public Guid TransactionId { get; private set; }
    }
}