﻿// Filename: ITransactionFaker.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public interface ITransactionFaker
    {
        IReadOnlyCollection<ITransaction> FakeTransactions(int count);

        ITransaction FakeTransaction();
    }
}
