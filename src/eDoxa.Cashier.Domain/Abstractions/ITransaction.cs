// Filename: ITransaction.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface ITransaction : IEntity<TransactionId>
    {
        DateTime Timestamp { get; }

        Currency Currency { get; }

        TransactionType Type { get; }

        TransactionStatus Status { get; }

        TransactionDescription Description { get; }

        TransactionFailure Failure { get; }

        void Complete();

        void Fail(string message);
    }
}
