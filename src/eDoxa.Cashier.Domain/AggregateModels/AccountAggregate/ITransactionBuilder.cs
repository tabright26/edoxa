// Filename: ITransactionBuilder.cs
// Date Created: 2019-12-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface ITransactionBuilder
    {
        ITransaction Build();
    }
}
