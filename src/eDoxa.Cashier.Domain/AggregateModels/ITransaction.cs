// Filename: ITransaction.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public interface ITransaction : IEntity<TransactionId>
    {
        DateTime Timestamp { get; }

        ICurrency Currency { get; }

        TransactionType Type { get; }

        TransactionStatus Status { get; }

        TransactionDescription Description { get; }

        void MarkAsSucceded();

        void MarkAsFailed();
    }
}
